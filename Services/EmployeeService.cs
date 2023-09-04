using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _dataContext;

    public EmployeeService(DataContext data)
    {
        _dataContext = data;
    }
    
    public async Task<bool> CreateEmployee(EmployeeDto emp, Image img)
    {
        try
        {
            var e = new Employee()
            {
                Firstname = emp.Firstname,
                Lastname = emp.Lastname,
                Branch = await _dataContext.Branches.FindAsync(emp.BranchId),
                Position = await _dataContext.Position.FindAsync(emp.PositionId),
                DateOfBirth = emp.DateOfBirth,
                Image = img,
                IsActive = true,
            };
            var u = new User()
            {
                UserName = emp.UserName,
                Password = "12345",
                IsActive = true,
                Role = await _dataContext.Roles.FindAsync(emp.RoleId)
            };
            await _dataContext.Users.AddAsync(u);
            
            await _dataContext.Employees.AddAsync(e);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }
   
    public async Task<List<Employee>> GetEmployees()
    {
        return await _dataContext.Employees
            .Where(e=> e.IsActive)
            .Include(e => e.Branch.Region)
            .Include(e => e.Branch.District)
            .Include(e=> e.Position )
            .Include(e=> e.Image)
            .ToListAsync();
    }
    public async Task<bool> UpdateEmployee(EmployeeDto emp, Image img, int id)
    {
        try
        {
            var e = await _dataContext.Employees.FindAsync(id);
            if (e == null)
            {
                return false;
            }
            e.Firstname = emp.Firstname;
            e.Lastname = emp.Lastname;
            e.Image = img;
            e.Position = await _dataContext.Position.FindAsync(emp.PositionId);
            e.Branch = await _dataContext.Branches.FindAsync(emp.BranchId);
            e.DateOfBirth = emp.DateOfBirth;
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return false;
        }
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        try
        {
            var emp =  await _dataContext.Employees
                .Include(e=> e.Image)
                .Include(e=> e.Branch.District.Region)
                .Include(e=>e.Position)
                .FirstOrDefaultAsync(e=> e.Id == id);
            if (emp == null)
            {
                return null;
            }
            return emp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
    public async Task<bool> DeleteEmployee(int id)
    {
        var emp = await _dataContext.Employees.FindAsync(id);
        if (emp == null)
        {
            return false;
        }
        emp.IsActive = false;
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<Employee> GetEmployeeByName(User dto)
    {
        return await _dataContext.Employees.FirstOrDefaultAsync(e =>
            e.Firstname == dto.Firstname 
            && e.Lastname == dto.Lastname);
    }
}