using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers;
[Route("api/v1/role")]
[ApiController]
[Authorize]
public class RoleController : GenericController<Role, RoleDto>
{

    public RoleController(IGenericService<Role, RoleDto> gen) : base(gen)
    {
        
    }
}