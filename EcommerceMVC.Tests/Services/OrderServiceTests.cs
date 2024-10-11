using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace EcommerceMVC.Services.Tests
{
    public class OrderServiceTest : IDisposable
    {
        private readonly EcommerceDbContext _context;
        private readonly OrderService _orderService;

        public OrderServiceTest()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            _context = new EcommerceDbContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            DatabaseSeeder.SeedingData(_context);

            _orderService = new OrderService(_context);
        }

        [Fact]
        public async Task CreateOrderAsync_ValidParameters_ShouldCreateOrder()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var items = new List<CartItem> { new CartItem { ProductId = 1 } };
            var order = new Order(userId);

            await _context.Users.AddAsync(new User { Id = userId.ToString() });
            await _context.SaveChangesAsync();
            // Act
            var result = await _orderService.CreateOrderAsync(userId, items);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(1, await _context.Orders.CountAsync());
        }

        [Fact]
        public async Task CreateOrderAsync_InvalidParameters_ShouldThrowArgumentException()
        {
            // Arrange
            var userId = Guid.Empty;
            var items = new List<CartItem>();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _orderService.CreateOrderAsync(userId, items));
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var order = new Order(userId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var orderId = order.OrderId;

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task GetOrderByIdAsync_InvalidOrderId_ShouldReturnNull()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOrdersByUser_ValidUserId_ShouldReturnOrders()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var orders = new List<Order> { new Order(userId) };
            _context.Orders.AddRange(orders);
            _context.SaveChanges();

            // Act
            var result = _orderService.GetOrdersByUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, order => Assert.Equal(userId, order.UserId));
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ValidOrderId_ShouldUpdateStatus()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var order = new Order(userId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var orderId = order.OrderId;

            // Act
            await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.CONFIRMED);

            // Assert
            var updatedOrder = await _context.Orders.FindAsync(orderId);
            Assert.NotNull(updatedOrder);
            Assert.Equal(OrderStatus.CONFIRMED, updatedOrder.Status);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_InvalidOrderId_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.UpdateOrderStatusAsync(orderId, 1));
        }

        [Fact]
        public async Task CancelOrderAsync_ValidOrderId_ShouldCancelOrder()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var order = new Order(userId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var orderId = order.OrderId;

            // Act
            await _orderService.CancelOrderAsync(orderId);

            // Assert
            Assert.Equal(OrderStatus.CANCELLED, order.Status);
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }
    }
}