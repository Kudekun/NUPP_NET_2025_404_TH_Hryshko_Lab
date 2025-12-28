namespace Cinema.Common.Interfaces;

/// <summary>
/// Generic CRUD Service Interface
/// </summary>
public interface ICrudService<T>
{
    void Create(T element);
    T Read(Guid id);
    IEnumerable<T> ReadAll();
    void Update(T element);
    void Remove(T element);

    // Бонусне завдання: серіалізація
    void Save(string filePath);
    void Load(string filePath);
}
