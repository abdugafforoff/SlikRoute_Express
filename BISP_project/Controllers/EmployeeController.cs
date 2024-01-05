using BIS_project.Helper;
using BIS_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BIS_project.Models;

namespace BIS_project.Controllers;
[Route("api/v1/employee")]
[ApiController]
[Authorize(Roles = "ADMIN")]
public class EmployeeController : ControllerBase
{
    private readonly IWebHostEnvironment environment;
    private readonly EmployeeService _employeeService;
    private FileSaver _fileSaver;

    public EmployeeController(EmployeeService service, IWebHostEnvironment env, FileSaver saver)
    {
        _employeeService = service;
        environment = env;
        _fileSaver = saver;
    }
    
    [HttpPost(Name = "createEmployee")]
    public async Task<IActionResult> CreateEmployee([FromForm] EmployeeModel model)
    {
        try
        {
            var img = await _fileSaver.FileSaveAsync(model.File, "employees/images");
            var result = await _employeeService.CreateEmployee(model.Employee, img);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet(Name = "getEmployees")]
    public async Task<List<Employee>> GetEmployees()
    {
        try
        {
            return await _employeeService.GetEmployees();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet("{id}",Name = "getEmployeeById")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        try
        {
            var e= await _employeeService.GetEmployeeById(id);
            if (e == null)
            {
                return BadRequest("Ushbu xodim bazadan topilmadi");
            }

            return Ok(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPut("{id}",Name = "updateEmployee")]
    public async Task<bool> UpdateEmployee(int id, [FromForm] EmployeeModel model)
    {
         var img = await _fileSaver.FileSaveAsync(model.File, "employees/images");
         await _employeeService.UpdateEmployee( model.Employee, img, id);
         return true;
    }

    [HttpDelete("{id}", Name = "deleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            return Ok(await _employeeService.DeleteEmployee(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}