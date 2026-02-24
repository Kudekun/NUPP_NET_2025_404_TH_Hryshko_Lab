namespace Library.Infrastructure.Models;

public class MemberModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime MemberSince { get; set; }

    // 1-до-1: у кожного читача є одна бібліотечна картка
    public LibraryCardModel? LibraryCard { get; set; }

    // 1-до-багатьох: у читача може бути багато видач
    public ICollection<LoanModel> Loans { get; set; } = new List<LoanModel>();
}
