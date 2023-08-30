using BIS_project.Controllers;
using BIS_project.Dtos;
using BIS_project.Models;
using BIS_project.Response;

namespace BIS_project.IServices;

public interface IAuthService
{
    public Task<User> UserLogin(UserDto user);
    public Task<object> RefreshToken();
}   