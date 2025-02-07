using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ServiceEntity
{
    [Key]
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    public int UnitId { get; set; }
    public UnitTypeEntity UnitType { get; set; } = null!;

    public ICollection<ProjectServiceJunctionEntity> ProjectServices { get; set; } = [];
}