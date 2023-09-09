using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using BIS_project.AppData;
using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.IServices;
using BIS_project.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MimeKit;

namespace BIS_project.Services;
public class OrderService : IOrderService
{
    private readonly DataContext _dataContext;

    public OrderService(DataContext data)
    {
        _dataContext = data;
    }
    
    public async Task<List<Order>> GetAllOrders()
    {
        return await _dataContext.Orders
            .Include(e => e.Client)
            .Include(e => e.Employees)
            .Include(e => e.FromDistrict.Region)
            .Include(e => e.ToDistrict.Region)
            .Include(e => e.ProductImages)
            .Include(e => e.EndImage)
            .ToListAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _dataContext.Orders.FindAsync(id);
    }

    public async Task<APIResponse> CreateBaseOrder(BaseOrderDto order)
    {
        try
        {
            var o = new Order()
            {
                EndPoint = order.EndPoint,
                StartPoint = order.StartPoint,
                FromDistrict = await _dataContext.Districts.FindAsync(order.FromDistrict),
                FromRegion = await _dataContext.Regions.FindAsync(order.FromRegion),
                ToDistrict = await _dataContext.Districts.FindAsync(order.ToDistrict),
                ToRegion = await _dataContext.Regions.FindAsync(order.ToRegion),
                Services = order.Services,
                LoadDayTime = order.LoadDayTime,
                FromLoadTime = order.FromLoadTime,
                ToLoadTime = order.ToLoadTime,
                PaymentType = order.PaymentType,
                HomeType = order.HomeType,
                CreatedAt = DateTime.UtcNow,
                Client = await CreteClient(order),
                Status = "CREATED"
            };
            if (o.Client == null)
            {
                return new APIResponse(500, "", "err");
            }
            
            await _dataContext.Orders.AddAsync(o);
            await _dataContext.SaveChangesAsync();
            
            await CreateUser(order);
            return new APIResponse(200, "Order saved", "");
        }
        catch (Exception e)
        {
            Console.WriteLine("Outer Exception: " + e.Message);
            if (e.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + e.InnerException.Message);
            }
            return new APIResponse(500, "", e.Message);
        }
    }

    public async Task<Client> CreteClient(BaseOrderDto order)
    {
        try
        {
            Client clientExists = await _dataContext.Client.FirstOrDefaultAsync(e => e.Name == order.FullName);

            if (clientExists == null)
            {
                var client = new Client
                {
                    Email = order.Email,
                    Name = order.FullName,
                    PhoneNumber = order.PhoneNumber,
                    TotalSpent = await CalculateTotalSpent(await _dataContext.Orders
                        .Where(e => e.Client.Name == order.FullName)
                        .ToListAsync()),
                    NumberOfOrders = await CalculateOrders(await _dataContext.Orders
                        .Where(e => e.Client.Name == order.FullName)
                        .ToListAsync())
                };
                await _dataContext.Client.AddAsync(client);
                await _dataContext.SaveChangesAsync();
                return client;
            }
            return clientExists;
           
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Order> UpdateOrder(string comment, List<Image> imgs)
    {
        return new Order();
    }
    
    private async Task<User> CreateUser(BaseOrderDto order)
    {
        try
        {
            var userExists = await _dataContext.Users.FirstOrDefaultAsync(e => e.UserName == order.Email);
    
            if (userExists == null)
            {
                var newUser = new User
                {
                    IsActive = true,
                    Firstname = order.FullName,
                    UserName = order.Email,
                    Lastname = order.FullName,
                    Password = GenerateRandomPassword(),
                    Role = await _dataContext.Roles.FirstOrDefaultAsync(e => e.Role_name == "CLIENT"),
                };
                await _dataContext.Users.AddAsync(newUser);
                await _dataContext.SaveChangesAsync();
                await SendEmail(order.Email, newUser.UserName, order.FullName, newUser.Password);

                return newUser;
            }
            await SendEmail(order.Email, userExists.UserName, order.FullName, userExists.Password);
            return userExists;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private async Task<int> CalculateOrders(List<Order> orders)
    {
        return orders.Count();
    }

    private async Task<int> CalculateTotalSpent(List<Order> orders)
    {
        int totalSpent = 0;
        foreach (var order in orders)
        {
            totalSpent = +order.TotalAmount;
        }
        return totalSpent;
    }
    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        int passwordLength = 6;
        string password = new string(Enumerable.Repeat(chars, passwordLength)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        return password;
    }

    public async Task  SendEmail(string toEmail, string username, string Name, string password)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("SilkRoute Express", "abdugafforovazimjon33@gmail.com"));
            message.To.Add(new MailboxAddress(username, toEmail));
            message.Subject = "Account credentials";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Dear {Name},\n\nThank you for using our service!\n\nYour login credentials are as follows:\n\n" +
                                   $"Username: **{username}**\nPassword: **{password}**\n\nWe appreciate your trust in our service and are here to assist you with any questions or concerns. If you have any feedback or need assistance, please don't hesitate to contact us.\n\nBest regards,\nThe SilkRoute Express Team";
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("abdugafforovazimjon33@gmail.com", "fzazphmwefownyub");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}