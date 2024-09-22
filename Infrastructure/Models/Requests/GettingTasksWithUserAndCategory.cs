namespace Infrastructure.Models.Requests;

public class GettingTasksWithUserAndCategory
{
    public Guid TaskId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid  UserId { get; set; }
    public string UserName { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
}