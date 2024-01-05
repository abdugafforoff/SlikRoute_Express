using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BIS_project.Controllers;
[Route("api/v1/truck")]
[ApiController]
public class TruckController : ControllerBase
{
    private readonly Validator _validation;
    private readonly TruckService _truckService;
    private readonly FileSaver _fileSaver;
    public TruckController(TruckService service, FileSaver saver, Validator validation)
    {
        _truckService = service;
        _fileSaver = saver;
        _validation = validation;
    }
    
    
    [HttpPost(Name = "CreateTruck")]
    public async Task<APIResponse> CreateTruck()
    {
        try
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var truckData = JsonConvert.DeserializeObject<TruckDto>(Request.Form["truck"]);
                bool isValid = await _validation.Validate(new TruckDto(), truckData);
                if (!isValid)
                {
                    var errResponse = new APIResponse(400, "Not valid object", "Parametrlar to'gri berilmagan!");
                    return errResponse;
                }
                var images = await _fileSaver.UploadFiles(Request.Form.Files, "truck/images");
              
                var result = await _truckService.CreateTruck(truckData, images);
                var res = new APIResponse(200, result, "");
                return res;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



    
    [HttpGet(Name = "GetAllTrucks")]
    public async Task<IActionResult> GetAllTrucks()
    {
        try
        {
            return Ok(await _truckService.GetAllTruck());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            BadRequest("Unknown error");
            throw;
        }
    }
}