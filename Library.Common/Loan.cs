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

    // конструктор з параметрами
    public Loan(Guid memberId, Guid itemId, int daysToReturn = 14)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        ItemId = itemId;
        LoanDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(daysToReturn);
    }

    // метод
    public bool IsOverdue() => DateTime.Now > DueDate;

    // метод
    public string GetInfo()
    {
        return $"Видача: Читач={MemberId} | Книга={ItemId} | До: {DueDate:dd.MM.yyyy} | Прострочено: {IsOverdue()}";
    }
}
