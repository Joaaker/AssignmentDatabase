namespace Business.Models;

public class ServiceModel
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public string UnitType { get; set; } = null!;
}