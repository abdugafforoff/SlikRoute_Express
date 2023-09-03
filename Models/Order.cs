using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIS_project.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public List<Employee> Employees { get; set; }
    public Driver Driver { get; set; }
    public Client Client { get; set; }
    public string Status { get; set; }// pending
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    [MaxLength(500)]
    public string Comment { get; set; }
    public string StartPoint { get; set; } // map startpoint
    public string EndPoint { get; set; }//map endpoint
    public int TotalAmount { get; set; } // means the amount of money of shipping
    public List<Image> ProductImages { get; set; }
    public List<Image> StartImage { get; set; }
    public List<Image> EndImage { get; set; }
    public int Rating { get; set; } // the client can rate it at the end of the ship
    public Region FromRegion { get; set; }
    public District FromDistrict { get; set; }
    public Region ToRegion { get; set; }
    public District ToDistrict { get; set; }
    public string LoadDayTime { get; set; }
    public DateOnly FromLoadTime { get; set; }
    public DateOnly ToLoadTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PaymentType { get; set; }// uzum pay, paymen, humo,
    public List<string> Services { get; set; } // packing, unpacking, car shipping, storage, debris removal,
    public string HomeType { get; set; } // apartment, house
}