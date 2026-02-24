using System.Collections;
using System.Text.Json;

namespace Library.Common;

public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
{
    private readonly List<T> _items = new();
    private readonly Func<T, Guid> _idSelector;
    private readonly string _filePath;

    // SemaphoreSlim для thread-safe доступу до колекції
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    // конструктор
    public CrudServiceAsync(Func<T, Guid> idSelector, string filePath = "data.json")
    {
        _idSelector = idSelector;
        _filePath = filePath;
    }

    public async Task<bool> CreateAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            _items.Add(element);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<T> ReadAsync(Guid id)
    {
        await _semaphore.WaitAsync();
        try
        {
            return _items.First(x => _idSelector(x) == id);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            return _items.ToList();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // пагінація
    public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        await _semaphore.WaitAsync();
        try
        {
            return _items.Skip((page - 1) * amount).Take(amount).ToList();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> UpdateAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            var id = _idSelector(element);
            var index = _items.FindIndex(x => _idSelector(x) == id);
            if (index < 0) return false;
            _items[index] = element;
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> RemoveAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            return _items.Remove(element);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // асинхронне збереження у файл
    public async Task<bool> SaveAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // IEnumerable реалізація
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
