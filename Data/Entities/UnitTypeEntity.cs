using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UnitTypeEntity
{
    [Key]
    public int Id { get; set; }

    public string UnitType { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];
}