namespace Cinema.Common.Models;

// делегат
public delegate void TicketSoldHandler(Ticket ticket);

/// <summary>
/// Клас Ticket
/// </summary>
public class Ticket
{
    // статичне поле
    private static int _totalTicketsSold;

    // статичний конструктор
    static Ticket()
    {
        _totalTicketsSold = 0;
    }

    // подія
    public static event TicketSoldHandler? OnTicketSold;

    // властивості
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public DateTime SessionTime { get; set; }
    public Guid? MovieId { get; set; }
    public Guid? CustomerId { get; set; }

    // конструктор без параметрів
    public Ticket()
    {
        Id = Guid.NewGuid();
        SessionTime = DateTime.Now;
        _totalTicketsSold++;
        OnTicketSold?.Invoke(this);
    }

    // конструктор з параметрами
    public Ticket(decimal price, string seatNumber, DateTime sessionTime, Guid? movieId = null, Guid? customerId = null) : this()
    {
        Price = price;
        SeatNumber = seatNumber;
        SessionTime = sessionTime;
        MovieId = movieId;
        CustomerId = customerId;
    }

    // метод
    public decimal ApplyDiscount(double discountPercent)
    {
        return Price * (1 - (decimal)discountPercent);
    }

    // метод
    public bool IsExpired()
    {
        return SessionTime < DateTime.Now;
    }

    // статичний метод
    public static int GetTotalTicketsSold()
    {
        return _totalTicketsSold;
    }

    // статичний метод
    public static decimal CalculateTotalRevenue(IEnumerable<Ticket> tickets)
    {
        return tickets.Sum(t => t.Price);
    }

    public override string ToString()
    {
        return $"[Ticket] Seat: {SeatNumber}, Price: {Price:C}, Session: {SessionTime:g}";
    }
}
