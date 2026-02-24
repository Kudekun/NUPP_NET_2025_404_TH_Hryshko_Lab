using Library.Common;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("=== Бібліотечна система — Лабораторна 2 ===\n");

// ============================================================
// 1. Паралельне створення 1000 книг через CrudServiceAsync
// ============================================================
Console.WriteLine("--- Паралельне створення 1000 книг ---");

var bookService = new CrudServiceAsync<Book>(b => b.Id, "books.json");

var tasks = new List<Task>();
Parallel.For(0, 1000, i =>
{
    var book = Book.CreateNew();
    tasks.Add(bookService.CreateAsync(book));
});
await Task.WhenAll(tasks);

var allBooks = (await bookService.ReadAllAsync()).Cast<Book>().ToList();
Console.WriteLine($"Створено книг: {allBooks.Count}");

// ============================================================
// 2. LINQ — статистика по числових полях
// ============================================================
Console.WriteLine("\n--- LINQ статистика по книгах ---");

Console.WriteLine($"Сторінки — Мін: {allBooks.Min(b => b.Pages)}, Макс: {allBooks.Max(b => b.Pages)}, Середнє: {allBooks.Average(b => b.Pages):F1}");
Console.WriteLine($"Рік      — Мін: {allBooks.Min(b => b.Year)}, Макс: {allBooks.Max(b => b.Year)}, Середнє: {allBooks.Average(b => b.Year):F1}");

var topAuthor = allBooks.GroupBy(b => b.Author).OrderByDescending(g => g.Count()).First();
Console.WriteLine($"Найпопулярніший автор: {topAuthor.Key} ({topAuthor.Count()} книг)");

// Пагінація — сторінка 1, по 5 елементів
var page1 = await bookService.ReadAllAsync(1, 5);
Console.WriteLine($"\nПагінація (сторінка 1, 5 елементів):");
foreach (var b in page1)
    Console.WriteLine($"  {b.GetInfo()}");

// ============================================================
// 3. Збереження колекції у файл (async)
// ============================================================
Console.WriteLine("\n--- Збереження у файл ---");
await bookService.SaveAsync();
Console.WriteLine("Збережено 1000 книг у books.json");

// ============================================================
// 4. Приклади примітивів синхронізації
// ============================================================
Console.WriteLine("\n--- Приклади синхронізації ---");

// Lock
Console.WriteLine("Lock:");
int lockCounter = 0;
object lockObj = new();
Parallel.For(0, 100, _ =>
{
    lock (lockObj)
    {
        lockCounter++;
    }
});
Console.WriteLine($"  Lock: лічильник після 100 паралельних інкрементів = {lockCounter}");

// Semaphore — обмежуємо до 3 одночасних потоків
Console.WriteLine("Semaphore:");
var semaphore = new SemaphoreSlim(3, 3);
int semCounter = 0;
var semTasks = Enumerable.Range(0, 10).Select(async i =>
{
    await semaphore.WaitAsync();
    try
    {
        Interlocked.Increment(ref semCounter);
        await Task.Delay(10);
    }
    finally
    {
        semaphore.Release();
    }
});
await Task.WhenAll(semTasks);
Console.WriteLine($"  Semaphore: виконано 10 завдань з обмеженням 3 одночасно, всього: {semCounter}");

// AutoResetEvent — сигналізація між потоками
Console.WriteLine("AutoResetEvent:");
var autoEvent = new AutoResetEvent(false);
string? receivedMessage = null;

var producer = Task.Run(() =>
{
    Thread.Sleep(50);
    receivedMessage = "Дані готові!";
    autoEvent.Set();
});

var consumer = Task.Run(() =>
{
    autoEvent.WaitOne();
    Console.WriteLine($"  AutoResetEvent: отримано сигнал — '{receivedMessage}'");
});

await Task.WhenAll(producer, consumer);

// ============================================================
// 5. Паралельне створення журналів + статистика
// ============================================================
Console.WriteLine("\n--- 500 журналів паралельно ---");
var magazineService = new CrudServiceAsync<Magazine>(m => m.Id, "magazines.json");
Parallel.For(0, 500, i =>
{
    var mag = Magazine.CreateNew();
    magazineService.CreateAsync(mag).Wait();
});

var allMags = (await magazineService.ReadAllAsync()).Cast<Magazine>().ToList();
Console.WriteLine($"Журналів: {allMags.Count}");
Console.WriteLine($"Випуск — Мін: {allMags.Min(m => m.IssueNumber)}, Макс: {allMags.Max(m => m.IssueNumber)}, Середнє: {allMags.Average(m => m.IssueNumber):F1}");

var topTopic = allMags.GroupBy(m => m.Topic).OrderByDescending(g => g.Count()).First();
Console.WriteLine($"Найпопулярніша тема: {topTopic.Key} ({topTopic.Count()} журналів)");

await magazineService.SaveAsync();
Console.WriteLine("Збережено 500 журналів у magazines.json");

Console.WriteLine("\n=== Готово! ===");
