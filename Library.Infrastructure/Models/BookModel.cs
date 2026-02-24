namespace Library.Infrastructure.Models;

// Наслідує LibraryItemModel — TPT (окрема таблиця Books)
public class BookModel : LibraryItemModel
{
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int Pages { get; set; }
}
