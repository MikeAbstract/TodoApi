namespace TodoApi.Infrastructure.Entities;

public class User
{
    public ICollection<TodoItem> TodoItems { get; set; }
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}