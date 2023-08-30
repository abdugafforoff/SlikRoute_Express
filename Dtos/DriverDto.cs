namespace BIS_project.Dtos;
using BIS_project.Models;

public class DriverDto
{
    public string DriverFullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public  int TruckId { get; set; }
}