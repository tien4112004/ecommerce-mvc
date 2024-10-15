using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Services;

public interface ISavedAddressService
{
    Task AddAsync(ShippingAddress address);
    Task UpdateAsync(ShippingAddress address);
    Task DeleteAsync(Guid id);
    Task<ShippingAddress> GetByIdAsync(Guid id);
    Task<IEnumerable<ShippingAddress>> GetByUserIdAsync(Guid userId);
    Task SetDefaultAddressAsync(Guid id, Guid userId);
}