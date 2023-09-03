using BIS_project.Models;

namespace BIS_project.Dtos;

public class BaseOrderDto
{
    public Region FromRegion { get; set; }
    public District FromDistrict { get; set; }
    public Region ToRegion { get; set; }
    public District ToDistrict { get; set; }
    public string LoadDayTime { get; set; }
    public DateOnly FromLoadTime { get; set; }
    public DateOnly ToLoadTime { get; set; }
    public string StartPoint { get; set; }
    public string EndPoint { get; set; } 
    public string PaymentType { get; set; } 
    public List<string> Services { get; set; } 
    public string HomeType { get; set; }
} 