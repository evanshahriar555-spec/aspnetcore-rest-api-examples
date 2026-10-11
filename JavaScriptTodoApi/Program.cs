using JavaScriptTodoApi.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<TodoStore>();
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
