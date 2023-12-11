using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIS_project.Models;

public class Branch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string BranchName { get; set; }
    public Region Region { get; set; }
    public District District { get; set; }
}