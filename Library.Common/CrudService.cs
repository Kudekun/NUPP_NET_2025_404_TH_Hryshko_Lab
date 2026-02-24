using System.Text.Json;

namespace Library.Common;

public class CrudService<T> : ICrudService<T> where T : class
{
    private readonly List<T> _items = new();
    private readonly Func<T, Guid> _idSelector;

    // конструктор
    public CrudService(Func<T, Guid> idSelector)
    {
        _idSelector = idSelector;
    }

    public void Create(T element)
    {
        _items.Add(element);
    }

    public T Read(Guid id)
    {
        return _items.First(x => _idSelector(x) == id);
    }

    public IEnumerable<T> ReadAll()
    {
        return _items;
    }

    public void Update(T element)
    {
        var id = _idSelector(element);
        var index = _items.FindIndex(x => _idSelector(x) == id);
        if (index >= 0)
            _items[index] = element;
    }

    public void Remove(T element)
    {
        _items.Remove(element);
    }

    // Save/Load — додаткове завдання
    public void Save(string filePath)
    {
        var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public void Load(string filePath)
    {
        if (!File.Exists(filePath)) return;
        var json = File.ReadAllText(filePath);
        var loaded = JsonSerializer.Deserialize<List<T>>(json);
        if (loaded != null)
        {
            _items.Clear();
            _items.AddRange(loaded);
        }
    }
}
