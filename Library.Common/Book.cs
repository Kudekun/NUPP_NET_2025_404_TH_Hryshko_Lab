namespace Library.Common;

public class Book : LibraryItem
{
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Pages { get; set; }

    // конструктор
    public Book()
    {
        Author = string.Empty;
        ISBN = string.Empty;
        Pages = 0;
    }

    // конструктор з параметрами
    public Book(string title, int year, string author, string isbn, int pages)
        : base(title, year)
    {
        Author = author;
        ISBN = isbn;
        Pages = pages;
    }

    // метод
    public override string GetInfo()
    {
        return $"Книга: {Title} | Автор: {Author} | ISBN: {ISBN} | Сторінок: {Pages} | Рік: {Year}";
    }
}
