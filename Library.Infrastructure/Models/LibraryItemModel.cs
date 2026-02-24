namespace Library.Infrastructure.Models;

// Базова модель для LibraryItem (TPT — Table-per-Type)
public class LibraryItemModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }

    // Навігаційна властивість (1-до-багатьох: LibraryItem → Loans)
    public ICollection<LoanModel> Loans { get; set; } = new List<LoanModel>();
}
