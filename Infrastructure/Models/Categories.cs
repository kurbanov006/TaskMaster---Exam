namespace Infrastructure.Models;

public class Categories
{
    public Guid Id { get; set; }
    public string Name { get; set; }=null!;
    public DateTime CreatedAt { get; set; }
}