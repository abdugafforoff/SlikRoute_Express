using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class DistrictController : GenericController<District, DistrictDto>
{
    
    public DistrictController(IGenericService<District, DistrictDto> generService) : base(generService)
    {
        
    }
}