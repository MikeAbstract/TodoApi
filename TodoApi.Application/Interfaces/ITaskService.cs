using TodoApi.Application.DTOs;

namespace TodoApi.Application.Interfaces;

public interface ITaskService
{
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId);
    Task<IEnumerable<TaskResponseDto>> GetAllAsync(int userId, bool? isCompleted = null, PaginationDto? pagination = null);
    Task<TaskResponseDto> GetByIdAsync(int id, int userId);
    Task<TaskResponseDto> UpdateAsync(int id, CreateTaskDto dto, int userId);
    Task DeleteAsync(int id, int userId);
}