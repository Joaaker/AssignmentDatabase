namespace Business.Dtos;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int ProjectStatusId { get; set; }
    public int CustomerId { get; set; }
    public int ProjectManagerId { get; set; }
}