using System.Globalization;
using Cinema.Common.Models;
using Cinema.Common.Services;
using Cinema.Common.Extensions;

namespace Cinema.Console;

class Program
{
    static void Main(string[] args)
    {
        // Встановлення української локалі для правильного відображення валюти (грн)
        CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;

        System.Console.WriteLine("===========================================");
        System.Console.WriteLine("    Cinema Management System - Lab 1");
        System.Console.WriteLine("===========================================\n");

        // Підписка на події
        Person.OnPersonCreated += person =>
            System.Console.WriteLine($"[Event] New person created: {person.GetFullName()}");

        Movie.OnMovieAdded += movie =>
            System.Console.WriteLine($"[Event] New movie added: {movie.Title}");

        Ticket.OnTicketSold += ticket =>
            System.Console.WriteLine($"[Event] Ticket sold: Seat {ticket.SeatNumber}");

        // ============================================
        // Демонстрація CRUD для Customer
        // ============================================
        System.Console.WriteLine("\n--- CRUD Service for Customers ---\n");

        var customerService = new CrudService<Customer>(c => c.Id);

        // Create - додавання клієнтів
        var customer1 = new Customer("Ivan", "Petrenko", "ivan@gmail.com", 500);
        var customer2 = new Customer("Maria", "Kovalenko", "maria@ukr.net", 1200);
        var customer3 = new Customer("Oleksandr", "Shevchenko", "alex@outlook.com", 100);

        customerService.Create(customer1);
        customerService.Create(customer2);
        customerService.Create(customer3);

        // ReadAll - отримання всіх клієнтів
        System.Console.WriteLine("\n[ReadAll] All customers:");
        foreach (var customer in customerService.ReadAll())
        {
            System.Console.WriteLine($"  - {customer}");
            // Використання методів розширення
            System.Console.WriteLine($"    Email domain: {customer.GetEmailDomain()}, Initials: {customer.GetInitials()}");
        }

        // Read - отримання конкретного клієнта
        System.Console.WriteLine($"\n[Read] Customer by ID: {customerService.Read(customer1.Id)}");

        // Update - оновлення клієнта
        customer1.LoyaltyPoints = 1500;
        customer1.IsVip = true;
        customerService.Update(customer1);

        // Remove - видалення клієнта
        customerService.Remove(customer3);

        System.Console.WriteLine("\n[ReadAll] Customers after update and remove:");
        foreach (var customer in customerService.ReadAll())
        {
            System.Console.WriteLine($"  - {customer}");
        }

        // ============================================
        // Демонстрація CRUD для Movie
        // ============================================
        System.Console.WriteLine("\n--- CRUD Service for Movies ---\n");

        var movieService = new CrudService<Movie>(m => m.Id);

        var movie1 = new Movie("Inception", 148, "Sci-Fi", 8.8);
        var movie2 = new Movie("The Dark Knight", 152, "Action", 9.0);
        var movie3 = new Movie("Interstellar", 169, "Sci-Fi", 8.6);

        movieService.Create(movie1);
        movieService.Create(movie2);
        movieService.Create(movie3);

        System.Console.WriteLine("\n[ReadAll] All movies:");
        foreach (var movie in movieService.ReadAll())
        {
            System.Console.WriteLine($"  - {movie}");
            System.Console.WriteLine($"    Is long movie: {movie.IsLongMovie()}");
        }

        // ============================================
        // Демонстрація CRUD для Employee
        // ============================================
        System.Console.WriteLine("\n--- CRUD Service for Employees ---\n");

        var employeeService = new CrudService<Employee>(e => e.Id);

        var employee1 = new Employee("Petro", "Sydorenko", "petro@cinema.com", "Manager", 25000);
        var employee2 = new Employee("Anna", "Bondarenko", "anna@cinema.com", "Cashier", 15000);

        employeeService.Create(employee1);
        employeeService.Create(employee2);

        System.Console.WriteLine("\n[ReadAll] All employees:");
        foreach (var employee in employeeService.ReadAll())
        {
            System.Console.WriteLine($"  - {employee}");
            System.Console.WriteLine($"    Yearly salary: {employee.CalculateYearlySalary():C}");
        }

        // ============================================
        // Демонстрація CRUD для Ticket
        // ============================================
        System.Console.WriteLine("\n--- CRUD Service for Tickets ---\n");

        var ticketService = new CrudService<Ticket>(t => t.Id);

        var ticket1 = new Ticket(150.00m, "A1", DateTime.Now.AddHours(2), movie1.Id, customer1.Id);
        var ticket2 = new Ticket(180.00m, "B5", DateTime.Now.AddHours(3), movie2.Id, customer2.Id);
        var ticket3 = new Ticket(160.00m, "C3", DateTime.Now.AddHours(4), movie3.Id, customer1.Id);

        ticketService.Create(ticket1);
        ticketService.Create(ticket2);
        ticketService.Create(ticket3);

        System.Console.WriteLine("\n[ReadAll] All tickets:");
        foreach (var ticket in ticketService.ReadAll())
        {
            System.Console.WriteLine($"  - {ticket}");
            System.Console.WriteLine($"    With VIP discount (15%): {ticket.ApplyDiscount(0.15):C}");
        }

        // Статичний метод для підрахунку виручки
        var totalRevenue = Ticket.CalculateTotalRevenue(ticketService.ReadAll());
        System.Console.WriteLine($"\n[Static Method] Total revenue: {totalRevenue:C}");

        // ============================================
        // Бонусне завдання: Save/Load
        // ============================================
        System.Console.WriteLine("\n--- Bonus: Save/Load (Serialization) ---\n");

        const string moviesFilePath = "movies.json";

        // Save - збереження у файл
        movieService.Save(moviesFilePath);

        // Створюємо новий сервіс та завантажуємо дані
        var newMovieService = new CrudService<Movie>(m => m.Id);
        newMovieService.Load(moviesFilePath);

        System.Console.WriteLine("\n[ReadAll] Movies loaded from file:");
        foreach (var movie in newMovieService.ReadAll())
        {
            System.Console.WriteLine($"  - {movie}");
        }

        // ============================================
        // Статистика
        // ============================================
        System.Console.WriteLine("\n--- Statistics ---\n");
        System.Console.WriteLine($"Total persons created: {Person.GetTotalPersonsCreated()}");
        System.Console.WriteLine($"Total customers: {Customer.GetTotalCustomers()}");
        System.Console.WriteLine($"Total employees: {Employee.GetTotalEmployees()}");
        System.Console.WriteLine($"Total movies: {Movie.GetTotalMovies()}");
        System.Console.WriteLine($"Total tickets sold: {Ticket.GetTotalTicketsSold()}");
        System.Console.WriteLine($"Total CRUD operations: {CrudService<object>.GetOperationsCount()}");

        System.Console.WriteLine("\n===========================================");
        System.Console.WriteLine("    Demo completed successfully!");
        System.Console.WriteLine("===========================================");
    }
}
