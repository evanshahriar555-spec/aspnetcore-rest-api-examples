using Microsoft.Extensions.Options;
using MongoBooksApi.Models;
using MongoDB.Driver;
namespace MongoBooksApi.Services;
public class BooksService
{
    private readonly IMongoCollection<Book> _books;
    public BooksService(IOptions<BookStoreDatabaseSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        _books = client.GetDatabase(settings.DatabaseName).GetCollection<Book>(settings.BooksCollectionName);
    }
    public async Task<List<Book>> GetAsync() => await _books.Find(_ => true).ToListAsync();
    public async Task<Book?> GetAsync(string id) => await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Book book) => await _books.InsertOneAsync(book);
    public async Task UpdateAsync(string id, Book book) => await _books.ReplaceOneAsync(b => b.Id == id, book);
    public async Task RemoveAsync(string id) => await _books.DeleteOneAsync(b => b.Id == id);
}
