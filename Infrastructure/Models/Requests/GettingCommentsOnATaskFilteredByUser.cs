namespace Infrastructure.Models.Requests;

public class GettingCommentsOnATaskFilteredByUser
{
    public Guid Id { get; set; }
    public int TaskId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}