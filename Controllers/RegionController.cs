using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BIS_project.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class RegionController : GenericController<Region, RegionDto>
{
    public RegionController(IGenericService<Region, RegionDto> genericService) : base(genericService)
    {
        
    }
    
}