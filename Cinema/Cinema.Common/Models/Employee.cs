namespace Cinema.Common.Models;

// клас Employee наслідується від Person
public class Employee : Person
{
    // статичне поле
    private static int _totalEmployees;

    // статичний конструктор
    static Employee()
    {
        _totalEmployees = 0;
    }

    public string Position { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }

    // конструктор
    public Employee() : base()
    {
        _totalEmployees++;
        HireDate = DateTime.Now;
    }

    // конструктор з параметрами
    public Employee(string firstName, string lastName, string email, string position, decimal salary)
        : base(firstName, lastName, email)
    {
        Position = position;
        Salary = salary;
        HireDate = DateTime.Now;
        _totalEmployees++;
    }

    // метод
    public decimal CalculateYearlySalary()
    {
        return Salary * 12;
    }

    // перевизначений метод
    public override string GetFullName()
    {
        return $"{base.GetFullName()} ({Position})";
    }

    // статичний метод
    public static int GetTotalEmployees()
    {
        return _totalEmployees;
    }

    public override string ToString()
    {
        return $"[Employee] {GetFullName()}, Salary: {Salary:C}, Hired: {HireDate:d}";
    }
}
