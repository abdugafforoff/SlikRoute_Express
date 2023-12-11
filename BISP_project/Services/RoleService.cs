using System.Diagnostics.CodeAnalysis;
using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class RoleService : IGenericService<Role, RoleDto>
{
    private readonly DataContext _dataContext;

    public RoleService(DataContext data)
    {
        _dataContext = data;
    }
    
    public async Task<List<Role>> GetRoles()
    {
        return await _dataContext.Roles.ToListAsync();
    }
    
    public async Task<List<Role>> GetAll()
    {
        return await _dataContext.Roles.ToListAsync();
    }

    public async Task<Role> GetById(int id)
    {
        return await _dataContext.Roles.FindAsync(id);
    }

    public async Task<bool> Insert(RoleDto item)
    {
        try
        {
            var r = new Role()
            {
                Role_name = item.Role_name
            };
            var rl = await _dataContext.Roles.AddAsync(r);
            await _dataContext.SaveChangesAsync();
            if (rl == null)
            {
                return false;
            }
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
            var e = await _dataContext.Roles.FindAsync(id);
            _dataContext.Remove(e);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return false;
        }
    }

    public async Task<Role> Update(int id, RoleDto item)
    {
        var r = await _dataContext.Roles.FindAsync(id);
        r.Role_name = item.Role_name;
        await _dataContext.SaveChangesAsync();
        return r;
    }
}