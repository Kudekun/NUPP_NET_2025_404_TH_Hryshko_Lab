namespace Library.Infrastructure.Models;

public class LoanModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }

    // FK до MemberModel (1 читач → багато видач)
    public int MemberId { get; set; }
    public MemberModel Member { get; set; } = null!;

    // FK до LibraryItemModel (1 позиція → багато видач)
    public int LibraryItemId { get; set; }
    public LibraryItemModel LibraryItem { get; set; } = null!;
}
