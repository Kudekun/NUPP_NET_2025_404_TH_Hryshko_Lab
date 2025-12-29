using System.Text.Json;
using Cinema.Common.Interfaces;

namespace Cinema.Common.Services;

public class CrudService<T> : ICrudService<T> where T : class
{
    // колекція для зберігання даних
    private readonly List<T> _items = new();

    private readonly Func<T, Guid> _idSelector;

    // статичне поле
    private static int _operationsCount;

    // статичний конструктор
    static CrudService()
    {
        _operationsCount = 0;
    }

    // конструктор
    public CrudService(Func<T, Guid> idSelector)
    {
        _idSelector = idSelector;
    }

    // метод Create
    public void Create(T element)
    {
        _items.Add(element);
        _operationsCount++;
        Console.WriteLine($"[CRUD] Created: {element}");
    }

    // метод Read
    public T Read(Guid id)
    {
        _operationsCount++;
        var item = _items.FirstOrDefault(x => _idSelector(x) == id);
        if (item == null)
        {
            throw new KeyNotFoundException($"Element with id {id} not found");
        }
        return item;
    }

    // метод ReadAll
    public IEnumerable<T> ReadAll()
    {
        _operationsCount++;
        return _items.ToList();
    }

    // метод Update
    public void Update(T element)
    {
        var id = _idSelector(element);
        var index = _items.FindIndex(x => _idSelector(x) == id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Element with id {id} not found");
        }
        _items[index] = element;
        _operationsCount++;
        Console.WriteLine($"[CRUD] Updated: {element}");
    }

    // метод Remove
    public void Remove(T element)
    {
        var id = _idSelector(element);
        var item = _items.FirstOrDefault(x => _idSelector(x) == id);
        if (item == null)
        {
            throw new KeyNotFoundException($"Element with id {id} not found");
        }
        _items.Remove(item);
        _operationsCount++;
        Console.WriteLine($"[CRUD] Removed: {element}");
    }

    // бонусне завдання: метод Save (серіалізація)
    public void Save(string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(_items, options);
        File.WriteAllText(filePath, json);
        _operationsCount++;
        Console.WriteLine($"[CRUD] Saved {_items.Count} items to {filePath}");
    }

    // бонусне завдання: метод Load (десеріалізація)
    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[CRUD] File {filePath} not found, nothing to load");
            return;
        }

        var json = File.ReadAllText(filePath);
        var items = JsonSerializer.Deserialize<List<T>>(json);
        if (items != null)
        {
            _items.Clear();
            _items.AddRange(items);
            _operationsCount++;
            Console.WriteLine($"[CRUD] Loaded {items.Count} items from {filePath}");
        }
    }

    // статичний метод
    public static int GetOperationsCount()
    {
        return _operationsCount;
    }

    public int Count => _items.Count;
}
