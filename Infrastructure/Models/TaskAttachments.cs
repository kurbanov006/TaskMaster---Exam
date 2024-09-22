namespace Infrastructure.Models;

public class TaskAttachments
{
	public Guid Id { get; set; }
	public Guid TaskId { get; set; }
	public string FilePath { get; set; } = null!;
	public DateTime CreatedAt { get; set; }
}
