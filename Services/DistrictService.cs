using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class DistrictService: IGenericService<District, DistrictDto>
{
   private readonly DataContext _dbContext;

   public DistrictService(DataContext data)
   {
      _dbContext = data;
   }

    
   private int CalculateOrder(List<Order> orders)
   {
      return orders.Count();
   }
    
   public async Task<List<District>> GetAll()
   {
      return await _dbContext.Districts.Include(e => e.Region).ToListAsync();
   }

   public async Task<District> GetById(int id)
   {
      return await _dbContext.Districts.FindAsync(id);
   }

   public async Task<bool> Insert(DistrictDto item)
   {
      var ds = new District()
      {
         Name = item.Name,
         NumberOfOrders = CalculateOrder(await _dbContext.Orders
            .Include(e => e.FromDistrict)
            .Where(e => e.FromDistrict.Name == item.Name)
            .ToListAsync()),
         Region = await _dbContext.Regions.FindAsync(item.RegionId)
      };
      var res =  await _dbContext.Districts.AddAsync(ds);
      await _dbContext.SaveChangesAsync();
      if (res == null)
      {
         return false;
      }
      return true;
   }



   public async Task<bool> Delete(int id)
   {
      var e = await _dbContext.Districts.FindAsync(id);
       _dbContext.Districts.Remove(e);
      await _dbContext.SaveChangesAsync();
      return true;
   }

   public async Task<District> Update(int id, DistrictDto dto)
   {
      District dis = await _dbContext.Districts.FindAsync(id);

      dis.Name = dto.Name;
      dis.NumberOfOrders = CalculateOrder(await _dbContext.Orders
         .Include(e => e.FromDistrict)
         .Where(e => e.FromDistrict.Name == dto.Name)
         .ToListAsync());
      dis.Region = await _dbContext.Regions.FindAsync(dto.RegionId);
      await _dbContext.SaveChangesAsync();
      return dis;
   }

   public async Task<List<District>> GetDistrictsById(int id)
   {
      return await _dbContext.Districts
         .Where(e => e.Region.Id == id)
         .ToListAsync();
   }
}