namespace Library.Common;

public class Member
{
    private static readonly string[] FirstNames = { "Олена", "Іван", "Марія", "Андрій", "Юлія", "Олексій", "Наталія", "Дмитро", "Ірина", "Василь" };
    private static readonly string[] LastNames = { "Петренко", "Коваль", "Шевченко", "Бондаренко", "Кравченко", "Лисенко", "Мельник", "Гриценко" };

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime MemberSince { get; set; }

    // конструктор
    public Member()
    {
        Id = Guid.NewGuid();
        FullName = string.Empty;
        Email = string.Empty;
        MemberSince = DateTime.Now;
    }

    public Member(string fullName, string email)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        MemberSince = DateTime.Now;
    }

    // статичний метод
    public static Member CreateNew()
    {
        var rnd = Random.Shared;
        var first = FirstNames[rnd.Next(FirstNames.Length)];
        var last = LastNames[rnd.Next(LastNames.Length)];
        return new Member(
            $"{first} {last}",
            $"{first.ToLower()}.{last.ToLower()}@email.com"
        );
    }

    public string GetInfo()
    {
        return $"Читач: {FullName} | Email: {Email} | Член з: {MemberSince:dd.MM.yyyy}";
    }
}
