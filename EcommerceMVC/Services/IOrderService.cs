using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string userId, List<CartItem> items, ShippingAddress address, string note);
    Task<Order> GetOrderByIdAsync(string orderId);
    IEnumerable<Order> GetOrdersByUser(string userId);
    Task UpdateOrderStatusAsync(string orderId, int status);
    Task CancelOrderAsync(string orderId);
    // decimal CalculateTotalAmount(List<CartItem> items);
}