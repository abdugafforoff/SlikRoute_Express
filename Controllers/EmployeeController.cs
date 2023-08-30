using System.Runtime.InteropServices.JavaScript;
using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
using BIS_project.Migrations;
using BIS_project.Models;

namespace BIS_project.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
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
    
    [HttpPost(Name = "UploadEmployee")]
    public async Task<IActionResult> UploadEmployee([FromForm] EmployeeModel model)
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

    [HttpGet(Name = "GetEmployees")]
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
    [HttpGet("{id}",Name = "GetEmployeeById")]
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
    
    
    [HttpPut("{id}",Name = "UpdateEmployee")]
    public async Task<bool> UpdateEmployee(int id, [FromForm] EmployeeModel model)
    {
         var img = await _fileSaver.FileSaveAsync(model.File, "employees/images");
         await _employeeService.UpdateEmployee( model.Employee, img, id);
         return true;
    }

    [HttpDelete("{id}", Name = "DeleteEmployee")]
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
            throw;
        }
    }


}