using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Data.Entities;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "date")] 
    public DateOnly StartDate { get; set; }

    [Column(TypeName = "date")] 
    public DateOnly? EndDate { get; set; }

    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    
    public int ProjectManagerId { get; set; }
    public EmployeeEntity ProjectManager { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];
}
