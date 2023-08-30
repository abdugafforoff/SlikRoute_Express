using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIS_project.Models;

public class Truck 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string TruckModel { get; set; }
    public string TruckNumber { get; set; }
    public string TruckStatus { get; set; }
    public List<Image> TruckImages { get; set; }
    public int ManufacturedYear { get; set; }   
}