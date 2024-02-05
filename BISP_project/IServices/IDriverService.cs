using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.Models;

namespace BIS_project.IServices;

public interface IDriverService
{
    public Task<bool> CreateDriver(DriverDto driver, Image img, Image license, int truckId);
    public Task<APIResponse> GetDrivers();
    public Task<APIResponse> DeleteDriver(int id);
}