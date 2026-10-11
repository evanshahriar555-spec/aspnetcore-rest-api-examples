using ControllerTodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllerTodoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TodoItemsController(TodoContext context) : ControllerBase
{
    private static TodoItemDTO ToDTO(TodoItem item) => new() { Id = item.Id, Name = item.Name, IsComplete = item.IsComplete };

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAll() =>
        await context.TodoItems.Select(item => new TodoItemDTO { Id = item.Id, Name = item.Name, IsComplete = item.IsComplete }).ToListAsync();

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TodoItemDTO>> Get(long id)
    {
        var item = await context.TodoItems.FindAsync(id);
        return item is null ? NotFound() : ToDTO(item);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> Create(TodoItemDTO dto)
    {
        var item = new TodoItem { Name = dto.Name, IsComplete = dto.IsComplete };
        context.TodoItems.Add(item);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, ToDTO(item));
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, TodoItemDTO dto)
    {
        if (id != dto.Id) return BadRequest("Route ID and body ID must match.");
        var item = await context.TodoItems.FindAsync(id);
        if (item is null) return NotFound();
        item.Name = dto.Name;
        item.IsComplete = dto.IsComplete;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var item = await context.TodoItems.FindAsync(id);
        if (item is null) return NotFound();
        context.TodoItems.Remove(item);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
