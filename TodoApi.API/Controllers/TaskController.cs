using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Infrastructure.Data;
using TodoApi.Application.DTOs;
namespace TodoApi.API.Controllers;
using System.Security.Claims;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskServices;

    public TasksController(ITaskService taskServices)
    {
        _taskServices = taskServices;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var result = await _taskServices.CreateAsync(dto, getUserId());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] bool? isCompleted = null, [FromQuery] PaginationDto? pagination = null)
    {
        var result = await _taskServices.GetAllAsync(getUserId(), isCompleted, pagination);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _taskServices.GetByIdAsync(id, getUserId());
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateTaskDto dto)
    {
        var result = await _taskServices.UpdateAsync(id, dto, getUserId());
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskServices.DeleteAsync(id, getUserId());
        return NoContent();
    }

    private int getUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                    ?? User.FindFirst("sub")
                    ?? User.FindFirst("id");

        if (claim == null)
            throw new UnauthorizedAccessException("The authenticated user could not be determined");

        return int.Parse(claim.Value);
    }
}