using System;
using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> CreateOrderAsync(Guid userId);
    Task<Order> GetOrderByIdAsync(Guid orderId);
    IEnumerable<Order> GetOrdersByUser(Guid userId);
    Task AddItemToOrderAsync(Guid orderId, int productId, int quantity);
    Task UpdateOrderStatusAsync(Guid orderId, int status);
}