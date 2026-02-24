namespace Library.Common;

public class Magazine : LibraryItem
{
    private static readonly string[] Titles = { "Країна", "Forbes Ukraine", "Vogue UA", "National Geographic", "Фокус", "Тиждень", "Корреспондент" };
    private static readonly string[] Publishers = { "Видавництво Країна", "Forbes Media", "Condé Nast", "National Geographic Society", "Фокус Медіа" };
    private static readonly string[] Topics = { "Суспільство", "Бізнес", "Мода", "Наука", "Політика", "Культура", "Технології" };

    public int IssueNumber { get; set; }
    public string Publisher { get; set; }
    public string Topic { get; set; }

    // конструктор
    public Magazine()
    {
        IssueNumber = 1;
        Publisher = string.Empty;
        Topic = string.Empty;
    }

    public Magazine(string title, int year, int issueNumber, string publisher, string topic)
        : base(title, year)
    {
        IssueNumber = issueNumber;
        Publisher = publisher;
        Topic = topic;
    }

    // статичний метод
    public static Magazine CreateNew()
    {
        var rnd = Random.Shared;
        return new Magazine(
            Titles[rnd.Next(Titles.Length)],
            rnd.Next(2000, 2025),
            rnd.Next(1, 52),
            Publishers[rnd.Next(Publishers.Length)],
            Topics[rnd.Next(Topics.Length)]
        );
    }

    public override string GetInfo()
    {
        return $"Журнал: {Title} | Випуск №{IssueNumber} | Видавець: {Publisher} | Тема: {Topic} | Рік: {Year}";
    }
}
