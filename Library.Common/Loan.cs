namespace Library.Common;

public class Loan
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }

    // конструктор
    public Loan()
    {
        Id = Guid.NewGuid();
        LoanDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(14);
    }

    public Loan(Guid memberId, Guid itemId, int daysToReturn = 14)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        ItemId = itemId;
        LoanDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(daysToReturn);
    }

    // статичний метод
    public static Loan CreateNew()
    {
        var rnd = Random.Shared;
        return new Loan(Guid.NewGuid(), Guid.NewGuid(), rnd.Next(7, 30));
    }

    // метод
    public bool IsOverdue() => DateTime.Now > DueDate;

    public string GetInfo()
    {
        return $"Видача: Читач={MemberId} | Книга={ItemId} | До: {DueDate:dd.MM.yyyy} | Прострочено: {IsOverdue()}";
    }
}
