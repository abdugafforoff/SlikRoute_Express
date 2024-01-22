using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers;
[Route("api/v1/district")]
[ApiController]
public class DistrictController : GenericController<District, DistrictDto>
{
    private readonly DistrictService _service;
    public DistrictController(IGenericService<District, DistrictDto> generService, DistrictService service) : base(generService)
    {   
        _service = service;
    }

    [HttpGet("byRegion/{id}", Name = "GetDistrictsById")]
    public async Task<List<District>> GetDistrictById(int id)
    {
        return await _service.GetDistrictsById(id);
    }
}