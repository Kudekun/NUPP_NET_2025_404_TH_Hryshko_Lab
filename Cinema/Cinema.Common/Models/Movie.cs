namespace Cinema.Common.Models;

// делегат
public delegate void MovieAddedHandler(Movie movie);

public class Movie
{
    // статичне поле
    private static int _totalMovies;

    // статичний конструктор
    static Movie()
    {
        _totalMovies = 0;
    }

    // подія
    public static event MovieAddedHandler? OnMovieAdded;

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; }

    // конструктор
    public Movie()
    {
        Id = Guid.NewGuid();
        _totalMovies++;
        OnMovieAdded?.Invoke(this);
    }

    // конструктор з параметрами
    public Movie(string title, int durationMinutes, string genre, double rating = 0) : this()
    {
        Title = title;
        DurationMinutes = durationMinutes;
        Genre = genre;
        Rating = rating;
    }

    // метод
    public string GetFormattedDuration()
    {
        int hours = DurationMinutes / 60;
        int minutes = DurationMinutes % 60;
        return $"{hours}h {minutes}m";
    }

    // метод
    public bool IsLongMovie()
    {
        return DurationMinutes > 120;
    }

    // статичний метод
    public static int GetTotalMovies()
    {
        return _totalMovies;
    }

    public override string ToString()
    {
        return $"[Movie] {Title} ({Genre}), Duration: {GetFormattedDuration()}, Rating: {Rating}/10";
    }
}
