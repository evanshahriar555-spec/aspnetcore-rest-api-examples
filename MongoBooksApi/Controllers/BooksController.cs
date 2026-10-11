using Microsoft.AspNetCore.Mvc;
using MongoBooksApi.Models;
using MongoBooksApi.Services;
using MongoDB.Bson;
namespace MongoBooksApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BooksController(BooksService service) : ControllerBase
{
    [HttpGet]
    public async Task<List<Book>> Get() => await service.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest("Invalid MongoDB ObjectId.");
        var book = await service.GetAsync(id);
        return book is null ? NotFound() : book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book book)
    {
        book.Id = null;
        await service.CreateAsync(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Book book)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest("Invalid MongoDB ObjectId.");
        if (await service.GetAsync(id) is null) return NotFound();
        book.Id = id;
        await service.UpdateAsync(id, book);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest("Invalid MongoDB ObjectId.");
        if (await service.GetAsync(id) is null) return NotFound();
        await service.RemoveAsync(id);
        return NoContent();
    }
}
