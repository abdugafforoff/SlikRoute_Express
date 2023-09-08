using BIS_project.Dtos;
using BIS_project.Models;

namespace BIS_project.IServices;

public interface IEmployeeService
{
    public Task<bool> CreateEmployee(EmployeeDto emp, Image img);
    public Task<List<Employee?>> GetEmployees();
    public Task<bool> UpdateEmployee(EmployeeDto emp, Image img, int id);
    public Task<Employee> GetEmployeeById(int id);
}