using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Services;

public class SavedAddressService : ISavedAddressService
{
    private readonly EcommerceDbContext _context;
    // private readonly ILogger<>
    
    public SavedAddressService(EcommerceDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(ShippingAddress address)
    {   
        await _context.SavedAddresses.AddAsync(address);
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(ShippingAddress address)
    {
        var savedAddress = _context.SavedAddresses.FindAsync(address.Id);
        if (savedAddress == null)
        {
            throw new InvalidOperationException("Address not found");
        }
        
        _context.SavedAddresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var savedAddress = await _context.SavedAddresses.FindAsync(id);
        if (savedAddress == null)
        {
            throw new InvalidOperationException("Address not found");
        }
        
        _context.SavedAddresses.Remove(savedAddress);
    }

    public async Task<ShippingAddress> GetByIdAsync(Guid id)
    {
        return await _context.SavedAddresses.FindAsync(id);
    }

    public async Task<IEnumerable<ShippingAddress>> GetByUserIdAsync(Guid userId)
    {   
        var addresses = _context.SavedAddresses.Where(a => a.UserId == userId);
        return addresses;
    }

    public Task SetDefaultAddressAsync(Guid id, Guid userId)
    {
        var addresses = _context.SavedAddresses.Where(a => a.UserId == userId);
        foreach (var address in addresses)
        {
            address.IsDefault = address.Id == id;
        }
        return _context.SaveChangesAsync();
    }
}