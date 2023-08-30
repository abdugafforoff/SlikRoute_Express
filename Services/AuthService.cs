using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.IServices;
using BIS_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _dbContext;    
    private readonly IConfiguration _configuration;

    public AuthService(DataContext data, IConfiguration config)
    {
        _dbContext = data;
        _configuration = config;
    }
    
    
    public async  Task<User> UserLogin(UserDto request)
    {
        if(request.Password != request.Password)
        {
            return null;
                   
        }
        var user = await _dbContext.Users.FirstOrDefaultAsync(e =>
            e.UserName == request.Username &&
            e.Password == request.Password &&
            e.IsActive);        
        
        return user;
    }

    public Task<object> RefreshToken()
    {
        throw new NotImplementedException();
    }
}