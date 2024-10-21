using System;
using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> CreateOrderAsync(string userId);
    Task<Order> GetOrderByIdAsync(string orderId);
    IEnumerable<Order> GetOrdersByUser(string userId);
    Task AddItemToOrderAsync(string orderId, int productId, int quantity);
    Task UpdateOrderStatusAsync(string orderId, int status);
}