namespace Cinema.Common.Models;

// делегат
public delegate void PersonCreatedHandler(Person person);

public class Person
{
    // статичне поле
    private static int _totalPersonsCreated;

    // статичний конструктор
    static Person()
    {
        _totalPersonsCreated = 0;
    }

    // подія
    public static event PersonCreatedHandler? OnPersonCreated;

    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // конструктор
    public Person()
    {
        Id = Guid.NewGuid();
        _totalPersonsCreated++;
        OnPersonCreated?.Invoke(this);
    }

    // конструктор з параметрами
    public Person(string firstName, string lastName, string email) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    // метод
    public virtual string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    // статичний метод
    public static int GetTotalPersonsCreated()
    {
        return _totalPersonsCreated;
    }

    public override string ToString()
    {
        return $"[Person] {GetFullName()} ({Email})";
    }
}
