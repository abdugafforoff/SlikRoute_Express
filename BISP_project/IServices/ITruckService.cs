using System.Diagnostics.Contracts;
using BIS_project.Dtos;
using BIS_project.Models;

namespace BIS_project.IServices;

public interface ITruckService
{
    public Task<Truck> CreateTruck(TruckDto truck, List<Image> img);
    public Task<bool> DeleteTruck(int id);
    public Task<Employee> UpdateTruck(TruckDto truck);
    public Task<List<Truck>> GetAllTruck();
}