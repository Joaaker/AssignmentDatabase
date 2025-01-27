using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}