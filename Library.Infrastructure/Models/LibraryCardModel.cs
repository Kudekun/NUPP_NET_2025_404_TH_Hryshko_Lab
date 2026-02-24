namespace Library.Infrastructure.Models;

// 1-до-1 з MemberModel
public class LibraryCardModel
{
    public int Id { get; set; }
    public string CardNumber { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }

    // FK до MemberModel
    public int MemberId { get; set; }
    public MemberModel Member { get; set; } = null!;
}
