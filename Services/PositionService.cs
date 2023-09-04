using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class PositionService : IGenericService<Position, PositionDto>
{
    private readonly DataContext _data;

    public PositionService(DataContext db)
    {
        _data = db;
    }
    public async Task<List<Position>> GetAll()
    {
        return await _data.Position.ToListAsync();
    }

    public async Task<Position> GetById(int id)
    {
        return await _data.Position.FindAsync(id);
    }

    public async Task<bool> Insert(PositionDto item)
    {
        try
        {
            var i = new Position()
            {
                Salary = item.Salary,
                Title = item.Title
            };
            await _data.Position.AddAsync(i);
            await _data.SaveChangesAsync();
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
            var p = await _data.Position.FindAsync(id);
             _data.Remove(p);
             await _data.SaveChangesAsync();
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Position> Update(int id, PositionDto item)
    {
        try
        {
            var ps = await _data.Position.FindAsync(id);
            ps.Salary = item.Salary;
            ps.Title = item.Title;
            await _data.SaveChangesAsync();
            return ps;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}