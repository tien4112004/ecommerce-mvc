using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace EcommerceMVC.Areas.Admin.Services;

public class OrderService : IOrderService
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<OrderService> _logger;

    public OrderService(EcommerceDbContext context, ILogger<OrderService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.OrderByDescending(order => order.CreatedTime).ToListAsync();
        return orders;
    }
    
    public async Task AddItemToOrderAsync(Guid orderId, int productId, int quantity)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            _logger.LogError($"Order with ID {orderId} not found.");
            throw new InvalidOperationException("Order not found.");
        }

        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            _logger.LogError($"Product with ID {productId} not found.");
            throw new InvalidOperationException("Product not found.");
        }

        var orderDetail = await _context.OrderDetails
            .Include(od => od.Product)
            .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
        if (orderDetail == null)
        {
            orderDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price
            };
            order.OrderDetails.Add(orderDetail);
        }
        else
        {
            orderDetail.Quantity += quantity;
        }

        order.TotalAmount = await CalculateTotalAmountAsync(orderId);
        await _context.SaveChangesAsync();
    }

    public async Task CancelOrderAsync(Guid orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            _logger.LogError($"Order with ID {orderId} not found.");
            throw new InvalidOperationException("Order not found.");
        }

        if (order.Status != OrderStatus.NOTPAID)
        {
            _logger.LogError($"Order with ID {orderId} cannot be cancelled.");
            throw new InvalidOperationException("Order cannot be cancelled.");
        }

        order.Status = OrderStatus.CANCELLED;
        await _context.SaveChangesAsync();
    }

    public Task<Order> CreateOrderAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(order => order.OrderDetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .FirstOrDefaultAsync(order => order.OrderId == orderId);
        if (order == null)
        {
            _logger.LogError($"Order with ID {orderId} not found.");
        }
        return order;
    }

    public IEnumerable<Order> GetOrdersByUser(Guid userId)
    {
        var orders = _context.Orders.Where(o => o.UserId == userId).ToList();
        return orders;
    }

    public Task UpdateOrderStatusAsync(Guid orderId, int status)
    {
        var order = _context.Orders.Find(orderId);
        if (order == null)
        {
            _logger.LogError($"Order with ID {orderId} not found.");
            throw new InvalidOperationException("Order not found.");
        }

        order.Status = status;
        return _context.SaveChangesAsync();
    }

    public async Task<decimal> CalculateTotalAmountAsync(Guid orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            _logger.LogError($"Order with ID {orderId} not found.");
            throw new InvalidOperationException("Order not found.");
        }

        decimal totalAmount = 0;
        var orderDetails = await _context.OrderDetails
            .Include(od => od.Product)
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
        foreach (var orderDetail in orderDetails)
        {
            totalAmount += orderDetail.TotalPrice;
        }
        return totalAmount;
    }
}