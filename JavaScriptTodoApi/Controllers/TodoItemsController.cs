using JavaScriptTodoApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace JavaScriptTodoApi.Controllers;
[ApiController]
[Route("api/todoitems")]
public class TodoItemsController(TodoStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll() => store.All();
    [HttpGet("{id:long}")]
    public ActionResult<TodoItem> Get(long id) => store.Get(id) is { } item ? item : NotFound();
    [HttpPost]
    public ActionResult<TodoItem> Post(TodoInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name)) return BadRequest("Name is required.");
        var item = store.Add(input);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }
    [HttpPut("{id:long}")]
    public IActionResult Put(long id, TodoInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name)) return BadRequest("Name is required.");
        return store.Update(id, input) ? NoContent() : NotFound();
    }
    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id) => store.Delete(id) ? NoContent() : NotFound();
}
