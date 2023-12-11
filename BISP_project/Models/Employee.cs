using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIS_project.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Image Image { get; set; }
    public Position Position { get; set; }
    public Branch Branch { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public string Username { get; set; }
}