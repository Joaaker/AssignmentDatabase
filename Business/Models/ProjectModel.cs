namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; } 
    public DateOnly? EndDate { get; set; }
    public string StatusType { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string ProjectManagerName { get; set; } = null!;
    public ICollection<ServiceModel> Services { get; set; } = [];
}