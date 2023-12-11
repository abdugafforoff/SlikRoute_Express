using BIS_project.Helper;
using BIS_project.Models;
using BIS_project.Request;
using BIS_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly DriverService _driverService;
    private readonly FileSaver _saver;

    public DriverController(DriverService service, FileSaver save)
    {
        _driverService = service;
        _saver = save;
    }

    [HttpPost(Name = "CreateDriver")]
    public async Task<APIResponse> CreateDriver([FromForm] DriverModel model)
    {
        var img = await _saver.FileSaveAsync(model.Photo, "Drivers/images");
        var license = await _saver.FileSaveAsync(model.License, "Drivers/images");
        var result = await _driverService.CreateDriver(model.Driver, img, license);
        return new APIResponse(200, result, "");
    }

    [HttpGet(Name = "GetDrivers")]
    public async Task<APIResponse> GetDrivers()
    {
        return await _driverService.GetDrivers();
    }
    
}