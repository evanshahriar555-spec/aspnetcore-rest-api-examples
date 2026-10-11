namespace JavaScriptTodoApi.Models;
public record TodoItem(long Id, string Name, bool IsComplete);
public record TodoInput(string Name, bool IsComplete);
public class TodoStore
{
    private readonly List<TodoItem> _items = new();
    private long _nextId = 1;
    private readonly object _gate = new();
    public List<TodoItem> All() { lock (_gate) return _items.ToList(); }
    public TodoItem? Get(long id) { lock (_gate) return _items.FirstOrDefault(x => x.Id == id); }
    public TodoItem Add(TodoInput input)
    {
        lock (_gate) { var item = new TodoItem(_nextId++, input.Name, input.IsComplete); _items.Add(item); return item; }
    }
    public bool Update(long id, TodoInput input)
    {
        lock (_gate)
        {
            var index = _items.FindIndex(x => x.Id == id);
            if (index < 0) return false;
            _items[index] = new TodoItem(id, input.Name, input.IsComplete);
            return true;
        }
    }
    public bool Delete(long id) { lock (_gate) return _items.RemoveAll(x => x.Id == id) > 0; }
}
