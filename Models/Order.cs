using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIS_project.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Employee Employee { get; set; }
    public Driver Driver { get; set; }
    public Client Client { get; set; }
    public string Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public string ProductStatus { get; set; }
    [MaxLength(500)] 
    public string Comment { get; set; }
    public string StartPoint { get; set; }
    public string EndPoint { get; set; }
    public string Deadline { get; set; }
    public int TotalAmount { get; set; }
    public List<Image> ProductImages { get; set; }
    public List<Image> StartImage { get; set; }
    public List<Image> EndImage { get; set; }
    public int Rating { get; set; }
    public Region FromRegion { get; set; }
    public District FromDistrict { get; set; }
    public Region ToRegion { get; set; }
    public District ToDistrict { get; set; }
    public DateTime LoadTime { get; set; }
    public DateOnly LoadDaytime { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool FlexibleLoad { get; set; }
    public DateOnly FromFlexibleDate { get; set; }
    public DateOnly ToFlexibleDate { get; set; }
    public string PaymentType { get; set; }
}