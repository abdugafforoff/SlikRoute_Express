using BIS_project.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Request;

public class TruckModel
{
    [FromForm(Name = "truck")]
    public TruckDto Truck { get; set; } 

    [FromForm(Name = "files")]
    public List<IFormFile> Files { get; set; }
}