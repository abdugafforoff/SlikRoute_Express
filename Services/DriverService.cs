using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class DriverService : IDriverService
{
    private readonly DataContext _dataContext;

    public DriverService(DataContext data)
    {
        _dataContext = data;
    }


    public async Task<bool> CreateDriver(DriverDto driver, Image img, Image license )
    {
        try
        {
            var d = new Driver()
            {
                Truck = await _dataContext.Trucks.FindAsync(driver.TruckId),
                Status = "Available to ship",
                DriverFullName = driver.DriverFullName,
                IsActive = true,
                DateOfBirth = driver.DateOfBirth,
                DriverPhoto = img,
                LicensePhoto = license,
                PhoneNumber = driver.PhoneNumber
            };
            await _dataContext.Drivers.AddAsync(d);
            await _dataContext.SaveChangesAsync();
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<APIResponse> GetDrivers()
    {
        try
        {
            var empList = await _dataContext.Drivers
                .Include(e=> e.LicensePhoto)
                .Include(e=> e.DriverPhoto)
                .Include(e=> e.Truck.TruckImages)
                .ToListAsync();
            return new APIResponse(200, empList, "");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<APIResponse> DeleteDriver(int id)
    {
        throw new NotImplementedException();
    }
}