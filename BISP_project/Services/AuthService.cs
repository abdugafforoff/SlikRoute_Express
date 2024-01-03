using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.Helper;
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
        try
        {
            var user = await _dbContext.Users
                .Include(e=> e.Role)
                .FirstOrDefaultAsync(e =>
                    e.UserName == request.Username &&
                    e.IsActive);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
       
       
    }
    public async Task<User> UserRegister(UserRegisterDto request)
    {
        try
        {
            var user = new User
            {
                UserName = request.Username,
                Password = request.Password,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Role = await _dbContext.Roles.FirstOrDefaultAsync(e => e.Role_name == "USER"),
                IsActive = true
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
   public async Task<APIResponse> ForgotPassword(string mail)
   {
       try
       {
           var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.UserName == mail);
           if (user == null)
           {
               return new APIResponse(400, null, "User not found!!");
           }
           var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
           user.Password = newPassword;
           await _dbContext.SaveChangesAsync();
           return new APIResponse(200, user, "");

       }
       catch(Exception e)
       {
           Console.WriteLine(e);
           return null;
       }
   }

    public Task<object> RefreshToken()
    {
        throw new NotImplementedException();
    }
}