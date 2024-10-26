using System.Data;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using EcommerceMVC.Services;

namespace EcommerceMVC.Services.Implementations;

/// <summary>
/// Service class for managing orders
/// </summary>
public class OrderService : IOrderService
{
    private readonly EcommerceDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public OrderService(EcommerceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="userId">The ID of the user placing the order.</param>
    /// <param name="items">The list of items in the cart.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created order.</returns>
    /// <exception cref="ArgumentException">Thrown when the input parameters are invalid.</exception>
    /// <exception cref="Exception">Thrown when there is an error creating the order.</exception>
    public async Task<Order> CreateOrderAsync(string userId, List<CartItem> items, ShippingAddress address, string note)
    {
        if (userId == Guid.Empty.ToString() || items == null || !items.Any())
            throw new ArgumentException("Invalid input parameters");

        Order order = new Order(userId, address, note);
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
                await transaction.RollbackAsync();
                throw new Exception($"Database error creating order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error creating order: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Gets an order by its ID.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the order.</returns>
    /// <exception cref="ArgumentException">Thrown when the order ID is empty.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the order is not found.</exception>
    public async Task<Order> GetOrderByIdAsync(string orderId)
    {
        if (orderId == Guid.Empty.ToString())
            throw new ArgumentException("Order ID cannot be empty", nameof(orderId));

        var order = await _context.Orders
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        // if (order == null)
        //     throw new KeyNotFoundException($"Order with ID {orderId} not found");

        return order;
    }

    /// <summary>
    /// Gets all orders for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of orders for the user.</returns>
    /// <exception cref="ArgumentException">Thrown when the user ID is empty.</exception>
    public IEnumerable<Order> GetOrdersByUser(string userId)
    {
        if (userId == Guid.Empty.ToString())
        {
            throw new ArgumentException("User ID cannot be empty", nameof(userId));
        }

        var orders = _context.Orders.Where(order => order.UserId == userId);
        return orders;
    }

    /// <summary>
    /// Updates the status of an order.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <param name="status">The new status of the order.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the order is not found.</exception>
    public async Task UpdateOrderStatusAsync(string orderId, int status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} was not found.");
        }

        order.Status = status;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Cancels an order.
    /// </summary>
    /// <param name="orderId">The ID of the order to cancel.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CancelOrderAsync(string orderId)
    {
        await UpdateOrderStatusAsync(orderId, OrderStatus.CANCELLED);
    }
}