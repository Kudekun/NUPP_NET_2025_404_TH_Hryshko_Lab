using Library.Infrastructure;
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("=== Бібліотечна система — Лабораторна 3 (EF Core + SQLite) ===\n");

// Налаштування контексту SQLite
var options = new DbContextOptionsBuilder<LibraryContext>()
    .UseSqlite("Data Source=library.db")
    .Options;

await using var context = new LibraryContext(options);
await context.Database.MigrateAsync();
Console.WriteLine("База даних library.db готова.\n");

var bookService = new BookDbService(context);
var memberRepo = new Repository<MemberModel>(context);
var cardRepo = new Repository<LibraryCardModel>(context);
var loanRepo = new Repository<LoanModel>(context);

// --- Create ---
Console.WriteLine("--- Create: додаємо читача з карткою та 3 книги ---");

var member = new MemberModel
{
    Guid = Guid.NewGuid(),
    FullName = "Олена Петренко",
    Email = "olena@email.com",
    MemberSince = DateTime.Now
};
await memberRepo.AddAsync(member);

var card = new LibraryCardModel
{
    CardNumber = $"CARD-{member.Id:D5}",
    IssuedAt = DateTime.Now,
    MemberId = member.Id
};
await cardRepo.AddAsync(card);
Console.WriteLine($"Читач: {member.FullName} | Картка: {card.CardNumber}");

var book1 = new BookModel { Guid = Guid.NewGuid(), Title = "Кобзар", Year = 1840, Author = "Тарас Шевченко", ISBN = "978-966-01-0001-1", Pages = 312 };
var book2 = new BookModel { Guid = Guid.NewGuid(), Title = "Тіні забутих предків", Year = 1913, Author = "Михайло Коцюбинський", ISBN = "978-966-01-0002-8", Pages = 128 };
var book3 = new BookModel { Guid = Guid.NewGuid(), Title = "Ворошиловград", Year = 2010, Author = "Сергій Жадан", ISBN = "978-966-01-0003-5", Pages = 448 };

await bookService.CreateAsync(book1);
await bookService.CreateAsync(book2);
await bookService.CreateAsync(book3);
Console.WriteLine($"Додано книг: 3");

// Loan — видача book1 читачеві
var loan = new LoanModel
{
    Guid = Guid.NewGuid(),
    MemberId = member.Id,
    LibraryItemId = book1.Id,
    LoanDate = DateTime.Now,
    DueDate = DateTime.Now.AddDays(14)
};
await loanRepo.AddAsync(loan);
Console.WriteLine($"Видача: {member.FullName} → '{book1.Title}'");

// --- ReadAll ---
Console.WriteLine("\n--- ReadAll: всі книги ---");
var allBooks = (await bookService.ReadAllAsync()).ToList();
foreach (var b in allBooks)
    Console.WriteLine($"  [{b.Id}] {b.Title} | {b.Author} | {b.Pages} стор. | {b.Year}");

// --- Read з пагінацією ---
Console.WriteLine("\n--- Пагінація (сторінка 1, 2 елементи) ---");
var page = await bookService.ReadAllAsync(1, 2);
foreach (var b in page)
    Console.WriteLine($"  {b.Title}");

// --- Update ---
Console.WriteLine("\n--- Update: змінюємо кількість сторінок у 'Кобзар' ---");
book1.Pages = 350;
await bookService.UpdateAsync(book1);
var updated = await bookService.ReadAsync(book1.Guid);
Console.WriteLine($"  Оновлено: {updated.Title} | Сторінок: {updated.Pages}");

// --- Delete ---
Console.WriteLine("\n--- Delete: видаляємо 'Ворошиловград' ---");
await bookService.RemoveAsync(book3);
var afterDelete = (await bookService.ReadAllAsync()).ToList();
Console.WriteLine($"  Книг після видалення: {afterDelete.Count}");
foreach (var b in afterDelete)
    Console.WriteLine($"  {b.Title}");

// --- Читачі з видачами ---
Console.WriteLine("\n--- Читачі з бібліотечними картками та видачами ---");
var members = await context.Members
    .Include(m => m.LibraryCard)
    .Include(m => m.Loans)
    .ThenInclude(l => l.LibraryItem)
    .ToListAsync();

foreach (var m in members)
{
    Console.WriteLine($"  {m.FullName} | Картка: {m.LibraryCard?.CardNumber ?? "немає"} | Видач: {m.Loans.Count}");
    foreach (var l in m.Loans)
        Console.WriteLine($"    → '{l.LibraryItem.Title}' до {l.DueDate:dd.MM.yyyy}");
}

Console.WriteLine("\n=== Готово! ===");
