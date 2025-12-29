namespace Cinema.Common.Models;

// клас Customer наслідується від Person
public class Customer : Person
{
    // статичне поле
    private static int _totalCustomers;

    // статичний конструктор
    static Customer()
    {
        _totalCustomers = 0;
    }

    public int LoyaltyPoints { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsVip { get; set; }

    // конструктор
    public Customer() : base()
    {
        _totalCustomers++;
        RegistrationDate = DateTime.Now;
        LoyaltyPoints = 0;
        IsVip = false;
    }

    // конструктор з параметрами
    public Customer(string firstName, string lastName, string email, int loyaltyPoints = 0)
        : base(firstName, lastName, email)
    {
        LoyaltyPoints = loyaltyPoints;
        RegistrationDate = DateTime.Now;
        IsVip = loyaltyPoints >= 1000;
        _totalCustomers++;
    }

    // метод
    public void AddLoyaltyPoints(int points)
    {
        LoyaltyPoints += points;
        if (LoyaltyPoints >= 1000)
        {
            IsVip = true;
        }
    }

    // метод
    public double GetDiscount()
    {
        return IsVip ? 0.15 : 0.05;
    }

    // статичний метод
    public static int GetTotalCustomers()
    {
        return _totalCustomers;
    }

    public override string ToString()
    {
        return $"[Customer] {GetFullName()}, Points: {LoyaltyPoints}, VIP: {IsVip}";
    }
}
