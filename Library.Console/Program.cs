using Library.Common;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Система управління бібліотекою ===\n");

// --- CRUD для книг ---
var bookService = new CrudService<Book>(b => b.Id);

// CREATE
Console.WriteLine("-- Додаємо книги --");
var book1 = new Book("Кобзар", 1840, "Тарас Шевченко", "978-966-01-0001-1", 312);
var book2 = new Book("Тіні забутих предків", 1913, "Михайло Коцюбинський", "978-966-01-0002-8", 128);
var book3 = new Book("Ворошиловград", 2010, "Сергій Жадан", "978-966-01-0003-5", 448);

bookService.Create(book1);
bookService.Create(book2);
bookService.Create(book3);

Console.WriteLine($"Додано: {book1.ToShortString()}");
Console.WriteLine($"Додано: {book2.ToShortString()}");
Console.WriteLine($"Додано: {book3.ToShortString()}");

// READ ALL
Console.WriteLine("\n-- Всі книги у сервісі --");
foreach (var book in bookService.ReadAll())
    Console.WriteLine($"  {book.GetInfo()}");

// READ ONE
Console.WriteLine($"\n-- Читаємо книгу за ID ({book2.Id}) --");
var found = bookService.Read(book2.Id);
Console.WriteLine($"  {found.GetInfo()}");

// UPDATE
Console.WriteLine("\n-- Оновлюємо книгу \"Кобзар\" (змінюємо кількість сторінок) --");
book1.Pages = 350;
bookService.Update(book1);
Console.WriteLine($"  Оновлено: {bookService.Read(book1.Id).GetInfo()}");

// REMOVE
Console.WriteLine("\n-- Видаляємо \"Ворошиловград\" --");
bookService.Remove(book3);
Console.WriteLine("  Після видалення:");
foreach (var book in bookService.ReadAll())
    Console.WriteLine($"  {book.GetInfo()}");

// --- CRUD для журналів ---
Console.WriteLine("\n--- CRUD для журналів ---");
var magazineService = new CrudService<Magazine>(m => m.Id);

var mag1 = new Magazine("Країна", 2024, 45, "Видавництво Країна", "Суспільство");
var mag2 = new Magazine("Forbes Ukraine", 2024, 12, "Forbes", "Бізнес");
magazineService.Create(mag1);
magazineService.Create(mag2);

Console.WriteLine("-- Всі журнали --");
foreach (var mag in magazineService.ReadAll())
    Console.WriteLine($"  {mag.GetInfo()}");

// --- CRUD для читачів ---
Console.WriteLine("\n--- CRUD для читачів ---");
var memberService = new CrudService<Member>(m => m.Id);

var member1 = new Member("Олена Петренко", "olena@email.com");
var member2 = new Member("Іван Коваль", "ivan@email.com");
memberService.Create(member1);
memberService.Create(member2);

Console.WriteLine("-- Всі читачі --");
foreach (var member in memberService.ReadAll())
    Console.WriteLine($"  {member.GetInfo()}");

// --- CRUD для видач ---
Console.WriteLine("\n--- CRUD для видач ---");
var loanService = new CrudService<Loan>(l => l.Id);

var loan1 = new Loan(member1.Id, book1.Id, 14);
var loan2 = new Loan(member2.Id, book2.Id, 7);
loanService.Create(loan1);
loanService.Create(loan2);

Console.WriteLine("-- Всі видачі --");
foreach (var loan in loanService.ReadAll())
    Console.WriteLine($"  {loan.GetInfo()}");

// Статичний метод
Console.WriteLine($"\n-- Загальна кількість створених елементів бібліотеки: {LibraryItem.GetTotalItems()} --");

// Save / Load (додаткове завдання)
Console.WriteLine("\n--- Save / Load ---");
string path = "books.json";
bookService.Save(path);
Console.WriteLine($"Збережено книги у файл: {path}");

var bookService2 = new CrudService<Book>(b => b.Id);
bookService2.Load(path);
Console.WriteLine($"Завантажено з файлу {path}:");
foreach (var book in bookService2.ReadAll())
    Console.WriteLine($"  {book.GetInfo()}");

Console.WriteLine("\n=== Готово! ===");
