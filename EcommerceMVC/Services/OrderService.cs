using System.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EcommerceMVC.Data.Services;

public class OrderService : IOrderService
{
    private readonly EcommerceDBContext _context;

    public OrderService(EcommerceDBContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Guid userId, List<CartItem> items)
    {
        if (userId == Guid.Empty || items == null || !items.Any())
            throw new ArgumentException("Invalid input parameters");

        Order order = new Order(userId);
        List<OrderDetail> orderDetails = items.Select(item => new OrderDetail(item, order.OrderId)).ToList();
        order.OrderDetails = orderDetails;
        order.TotalAmount = orderDetails.Sum(detail => detail.TotalPrice);
        
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
    
                await transaction.CommitAsync();
                return order;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Database error creating order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating order: {ex.Message}", ex);
            }
        }
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(order => order.OrderId == orderId);
        return order;
    }

    public IEnumerable<Order> GetOrdersByUser(Guid userId)
    {
        var orders = _context.Orders.Where(order => order.UserId == userId);
        return orders;
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, int status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} was not found.");
        }

        order.Status = status;
        await _context.SaveChangesAsync();
    }

    public async Task CancelOrderAsync(Guid orderId)
    {
        await UpdateOrderStatusAsync(orderId, OrderStatus.CANCELLED);
    }

    // public decimal CalculateTotalAmount(List<CartItem> items)
    // {
    //     foreach (var item in items)
    //     {
    //         
    //     }
    // }
}