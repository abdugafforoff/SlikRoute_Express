using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers;

[Route("api/v1/position")]
[ApiController]
public class PositionController : GenericController<Position, PositionDto>
{
    public PositionController(IGenericService<Position, PositionDto> ps) : base(ps)
    {
        
    }
}