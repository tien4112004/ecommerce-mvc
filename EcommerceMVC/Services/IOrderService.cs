using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Guid userId, List<CartItem> items);
    Task<Order> GetOrderByIdAsync(Guid orderId);
    IEnumerable<Order> GetOrdersByUser(Guid userId);
    Task UpdateOrderStatusAsync(Guid orderId, int status);
    Task CancelOrderAsync(Guid orderId);
    // decimal CalculateTotalAmount(List<CartItem> items);
}