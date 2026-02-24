namespace Library.Common;

public class Member
{
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

    // конструктор з параметрами
    public Member(string fullName, string email)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        MemberSince = DateTime.Now;
    }

    // метод
    public string GetInfo()
    {
        return $"Читач: {FullName} | Email: {Email} | Член з: {MemberSince:dd.MM.yyyy}";
    }
}
