using Microsoft.EntityFrameworkCore;
namespace ControllerTodoApi.Models;
public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}
