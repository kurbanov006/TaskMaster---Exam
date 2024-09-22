namespace Infrastructure.Models.Requests;

// Request 2
public class GettingCategoriesWithNumberOfTasks
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int CountTask { get; set; }
}