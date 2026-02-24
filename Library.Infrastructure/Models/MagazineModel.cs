namespace Library.Infrastructure.Models;

// Наслідує LibraryItemModel — TPT (окрема таблиця Magazines)
public class MagazineModel : LibraryItemModel
{
    public int IssueNumber { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
}
