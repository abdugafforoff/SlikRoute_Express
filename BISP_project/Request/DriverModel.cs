using BIS_project.Dtos;
using BIS_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Request;

public class DriverModel
{
    [FromForm(Name = "driver")]
    public DriverDto Driver { get; set; }
    
    [FromForm(Name = "files")]
    public IFormFile Photo { get; set; } 
    
    [FromForm(Name = "license")]
    public IFormFile License { get; set; }
    
    [FromForm(Name = "truckImages")]
    public List<IFormFile> TruckImages { get; set; }
  
}