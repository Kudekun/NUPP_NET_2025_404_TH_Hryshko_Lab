namespace Library.Common;

// метод розширення
public static class LibraryItemExtensions
{
    // метод розширення для виводу короткого опису
    public static string ToShortString(this LibraryItem item)
    {
        return $"'{item.Title}' ({item.Year}) [ID: {item.Id}]";
    }
}
