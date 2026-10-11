using MongoBooksApi.Models;
using MongoBooksApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<BookStoreDatabaseSettings>(builder.Configuration.GetSection("BookStoreDatabase"));
builder.Services.AddSingleton<BooksService>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
