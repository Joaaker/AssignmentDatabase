
namespace Data.Entities;

public class ProjectServiceJunctionEntity
{
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;

    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
}