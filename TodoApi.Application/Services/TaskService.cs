using Microsoft.EntityFrameworkCore;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Infrastructure.Data;
using TodoApi.Infrastructure.Entities;


namespace TodoApi.Application.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _dbContext;

    public TaskService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Methods
    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId)
    {
        var task = new TodoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsComplete = true,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _dbContext.TodoItems.Add(task);
        await _dbContext.SaveChangesAsync();

        return new TaskResponseDto
        {
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsComplete,
            CreatedAt = task.CreatedAt
        };
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(int userId, bool? isCompleted = null, PaginationDto? pagination = null)
    {
        var query = _dbContext.TodoItems
            .Where(t => t.UserId == userId);

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsComplete == isCompleted.Value);
        }

        if (pagination != null)
        {
            query = query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }

        var tasks = await query.ToListAsync();

        return tasks.Select(task => new TaskResponseDto
        {
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsComplete,
            CreatedAt = task.CreatedAt
        });
    }

    public async Task<TaskResponseDto> GetByIdAsync(int id, int userId)
    {
        var task = await _dbContext.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            throw new Exception("Task not found");
        }

        return new TaskResponseDto
        {
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsComplete,
            CreatedAt = task.CreatedAt
        };
    }

    public async Task<TaskResponseDto> UpdateAsync(int id, CreateTaskDto dto, int userId)
    {
        var task = await _dbContext.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            throw new Exception("Task not found");
        }

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.IsComplete = dto.IsCompleted;
        await _dbContext.SaveChangesAsync();
        return new TaskResponseDto
        {
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsComplete,
        };
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var task = await _dbContext.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            throw new Exception("Task not found");
        }

        _dbContext.TodoItems.Remove(task);
        await _dbContext.SaveChangesAsync();
    }
}