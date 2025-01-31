namespace Business.Dtos;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string ProjectStatus { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string ProjectManagerName { get; set; } = null!;
}