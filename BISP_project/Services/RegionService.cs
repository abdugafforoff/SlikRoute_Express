using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BIS_project.Services;

public class RegionService : IGenericService<Region, RegionDto>
{
    private readonly DataContext _dbContext;

    public RegionService(DataContext data)
    {
        _dbContext = data;
    }
    
    private int CalculateOrders(List<Order> orders)
    {
        return orders.Count();
    }
    
    public async Task<List<Region>> GetAll()
    {
        return await _dbContext.Regions.ToListAsync();
    }

    public async Task<Region> GetById(int id)
    {
        return await _dbContext.Regions.FindAsync(id);
    }

    public async Task<bool> Insert(RegionDto item)
    {
        try
        {
            var region = new Region()
            {
                RegionName = item.Name,
                NumberOfOrders = CalculateOrders(await _dbContext.Orders
                    .Include(e=> e.FromRegion)
                    .Where(e=> e.FromRegion.RegionName == item.Name)
                    .ToListAsync())
            };
            var result = await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            if (result == null)
            {
                return false;
            }

            return true;

        }
        catch(Exception ex)
        {
            return false;
        }

    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var e = await _dbContext.Regions.FindAsync(id);
            _dbContext.Remove(e);
            _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return false;
        }
    }

    public async Task<Region> Update(int id, RegionDto item)
    {
        var e = await _dbContext.Regions.FindAsync(id);
        e.RegionName = item.Name;
        e.NumberOfOrders = CalculateOrders(await _dbContext.Orders
            .Include(e => e.FromRegion)
            .Where(e => e.FromRegion.RegionName == item.Name)
            .ToListAsync());
        await _dbContext.SaveChangesAsync();
        return e;
    }
}