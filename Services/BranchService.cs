using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class BranchService : IGenericService<Branch, BranchDto>
{
    private readonly DataContext _dataContext;

    public BranchService(DataContext data)
    {
        _dataContext = data;
    }
    public async Task<List<Branch>> GetAll()
    {
        return await _dataContext.Branches
            .Include(e=> e.District)
            .ToListAsync();
    }
    public async Task<Branch> GetById(int id)
    {
        return await _dataContext.Branches
            .Include(e => e.District)
            .FirstOrDefaultAsync(e=> e.Id == id);
    }
    public async Task<bool> Insert(BranchDto item)
    {
        try
        {
            var b = new Branch()
            {
                District = await _dataContext.Districts.FindAsync(item.DistrictId),
                Name = item.Name,
                Region = await _dataContext.Regions.FindAsync(item.RegionId)
            };
            await _dataContext.Branches.AddAsync(b);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var i = await _dataContext.Branches.FindAsync(id);
            _dataContext.Branches.Remove(i);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Branch> Update(int id, BranchDto item)
    {
        try
        {
            var b = await _dataContext.Branches.FindAsync(id);
            b.District = await _dataContext.Districts.FindAsync(item.DistrictId);
            b.Region = await _dataContext.Regions.FindAsync(item.RegionId);
            b.Name = item.Name;
            await _dataContext.SaveChangesAsync();
            return b;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
       
    }
}