namespace Cinema.Common.Extensions;

using Cinema.Common.Models;

/// <summary>
/// Методи розширення для класу Person
/// </summary>
public static class PersonExtensions
{
    // метод розширення
    public static string GetEmailDomain(this Person person)
    {
        if (string.IsNullOrEmpty(person.Email) || !person.Email.Contains('@'))
        {
            return string.Empty;
        }

        return person.Email.Split('@')[1];
    }

    // метод розширення
    public static bool HasValidEmail(this Person person)
    {
        return !string.IsNullOrEmpty(person.Email) && person.Email.Contains('@');
    }

    // метод розширення
    public static string GetInitials(this Person person)
    {
        var firstInitial = !string.IsNullOrEmpty(person.FirstName) ? person.FirstName[0].ToString().ToUpper() : "";
        var lastInitial = !string.IsNullOrEmpty(person.LastName) ? person.LastName[0].ToString().ToUpper() : "";
        return $"{firstInitial}{lastInitial}";
    }
}
