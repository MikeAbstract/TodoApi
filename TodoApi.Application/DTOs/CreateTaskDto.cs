namespace TodoApi.Application.DTOs;

public class CreateTaskDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}