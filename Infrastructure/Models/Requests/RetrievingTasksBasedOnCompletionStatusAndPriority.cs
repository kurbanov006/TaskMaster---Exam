namespace Infrastructure.Models.Requests;

public class RetrievingTasksBasedOnCompletionStatusAndPriority
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedAt { get; set; }
}