namespace Library.Common;

// делегат
public delegate void ItemChangedHandler(string message);

public abstract class LibraryItem
{
    // статичне поле
    private static int _totalItems = 0;

    // статичний конструктор
    static LibraryItem()
    {
        _totalItems = 0;
    }

    // подія
    public event ItemChangedHandler? OnItemChanged;

    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }

    // конструктор
    public LibraryItem()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        Year = DateTime.Now.Year;
        _totalItems++;
    }

    public LibraryItem(string title, int year)
    {
        Id = Guid.NewGuid();
        Title = title;
        Year = year;
        _totalItems++;
    }

    public virtual string GetInfo()
    {
        OnItemChanged?.Invoke($"GetInfo викликано для: {Title}");
        return $"[{Id}] {Title} ({Year})";
    }

    // статичний метод
    public static int GetTotalItems() => _totalItems;
}
