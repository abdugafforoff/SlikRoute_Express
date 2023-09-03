using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BIS_project.Dtos;
using BIS_project.Models;
using BIS_project.Response;
using BIS_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BIS_project.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase 
{
    private readonly IConfiguration _configuration;
    private readonly AuthService _authService;

    public AuthController(AuthService service, IConfiguration configuration)
    {
        _authService = service;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<object> Login(UserDto request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username or password is missing.");
        }

        try
        {
            User user = await _authService.UserLogin(request);
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Role_name)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds); 

            var bearer = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new {user.Id, user.UserName, user.Role, bearer });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the login request.");
        }
    }
    
}