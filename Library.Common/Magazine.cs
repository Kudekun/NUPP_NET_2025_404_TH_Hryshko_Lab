namespace Library.Common;

public class Magazine : LibraryItem
{
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

    // конструктор з параметрами
    public Magazine(string title, int year, int issueNumber, string publisher, string topic)
        : base(title, year)
    {
        IssueNumber = issueNumber;
        Publisher = publisher;
        Topic = topic;
    }

    // метод
    public override string GetInfo()
    {
        return $"Журнал: {Title} | Випуск №{IssueNumber} | Видавець: {Publisher} | Тема: {Topic} | Рік: {Year}";
    }
}
