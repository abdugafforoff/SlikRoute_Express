using System.Text;
using BIS_project.AppData;
using BIS_project.Controllers;
using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.IServices;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddScoped<DistrictService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<IGenericService<Region, RegionDto>, RegionService>();
        builder.Services.AddScoped<IGenericService<District, DistrictDto>,DistrictService >();
        builder.Services.AddScoped<IGenericService<Role, RoleDto>, RoleService>();
        builder.Services.AddScoped<IGenericService<Position, PositionDto>, PositionService>();
        builder.Services.AddScoped<IGenericService<Branch, BranchDto>, BranchService>();
        builder.Services.AddScoped<EmployeeService>();
        builder.Services.AddScoped<FileSaver>();
        builder.Services.AddScoped<Validator>();
        builder.Services.AddScoped<TruckService>();
        builder.Services.AddScoped<DriverService>();
        builder.Services.AddScoped<OrderService>();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Startup", Version = "v1"});

        });

        builder.Host.UseSerilog((context, configuration) =>
        {
            var logFileName = $"log-{DateTime.Now:yyyy.MM.dd}.txt";

            configuration
                .WriteTo.File(
                    Path.Combine(logFileName),
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.Console()
                .MinimumLevel.Information();
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                RequireSignedTokens = true
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString);
    
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) =>
            {
                return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
            })));
        });
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(); 
        }
        app.UseStaticFiles();
        app.UseAuthentication();    
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}

