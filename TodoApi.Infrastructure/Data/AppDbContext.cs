using TodoApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace TodoApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
}