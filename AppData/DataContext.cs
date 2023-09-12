using BIS_project.Models;
using Microsoft.EntityFrameworkCore;


namespace BIS_project.AppData;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Branch> Branches { get; set; }
    public DbSet<Client?> Client { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Image>Images { get; set; }
    public DbSet<Order?> Orders { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Employee?> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<User> Users { get; set; }
}