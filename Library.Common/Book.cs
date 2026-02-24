namespace Library.Common;

public class Book : LibraryItem
{
    private static readonly string[] Titles = { "Кобзар", "Тіні забутих предків", "Ворошиловград", "Солодка Даруся", "Музей покинутих секретів", "Доця", "Ворота", "Чорний ворон", "Польові дослідження" };
    private static readonly string[] Authors = { "Тарас Шевченко", "Михайло Коцюбинський", "Сергій Жадан", "Марія Матіос", "Оксана Забужко", "Тамара Горіха Зерня", "Люко Дашвар" };

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

    public Book(string title, int year, string author, string isbn, int pages)
        : base(title, year)
    {
        Author = author;
        ISBN = isbn;
        Pages = pages;
    }

    // статичний метод
    public static Book CreateNew()
    {
        var rnd = Random.Shared;
        return new Book(
            Titles[rnd.Next(Titles.Length)],
            rnd.Next(1900, 2025),
            Authors[rnd.Next(Authors.Length)],
            $"978-{rnd.Next(100, 999)}-{rnd.Next(10000, 99999)}-{rnd.Next(0, 9)}",
            rnd.Next(100, 800)
        );
    }

    public override string GetInfo()
    {
        return $"Книга: {Title} | Автор: {Author} | ISBN: {ISBN} | Сторінок: {Pages} | Рік: {Year}";
    }
}
