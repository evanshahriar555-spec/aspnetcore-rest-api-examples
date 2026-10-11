using ControllerTodoApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoList"));
var app = builder.Build();
app.MapControllers();
app.Run();
