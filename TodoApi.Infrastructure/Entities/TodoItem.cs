namespace TodoApi.Infrastructure.Entities;

public class TodoItem
{
    public User User { get; set; }
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    
}