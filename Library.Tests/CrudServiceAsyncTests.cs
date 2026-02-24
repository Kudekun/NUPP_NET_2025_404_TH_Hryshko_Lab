using Library.Common;

namespace Library.Tests;

public class CrudServiceAsyncTests
{
    private CrudServiceAsync<Book> CreateService() =>
        new CrudServiceAsync<Book>(b => b.Id, "test_books.json");

    [Fact]
    public async Task CreateAsync_AddsElement()
    {
        var service = CreateService();
        var book = Book.CreateNew();

        var result = await service.CreateAsync(book);

        Assert.True(result);
        var all = await service.ReadAllAsync();
        Assert.Single(all);
    }

    [Fact]
    public async Task ReadAsync_ReturnsCorrectElement()
    {
        var service = CreateService();
        var book = Book.CreateNew();
        await service.CreateAsync(book);

        var found = await service.ReadAsync(book.Id);

        Assert.Equal(book.Id, found.Id);
        Assert.Equal(book.Title, found.Title);
    }

    [Fact]
    public async Task UpdateAsync_ChangesElement()
    {
        var service = CreateService();
        var book = Book.CreateNew();
        await service.CreateAsync(book);

        book.Pages = 999;
        await service.UpdateAsync(book);

        var updated = await service.ReadAsync(book.Id);
        Assert.Equal(999, updated.Pages);
    }

    [Fact]
    public async Task RemoveAsync_DeletesElement()
    {
        var service = CreateService();
        var book = Book.CreateNew();
        await service.CreateAsync(book);

        var result = await service.RemoveAsync(book);

        Assert.True(result);
        var all = await service.ReadAllAsync();
        Assert.Empty(all);
    }

    [Fact]
    public async Task ReadAllAsync_Pagination_ReturnsCorrectPage()
    {
        var service = CreateService();
        for (int i = 0; i < 10; i++)
            await service.CreateAsync(Book.CreateNew());

        var page2 = (await service.ReadAllAsync(2, 3)).ToList();

        Assert.Equal(3, page2.Count);
    }

    [Fact]
    public async Task CreateAsync_ThreadSafe_AllItemsAdded()
    {
        var service = CreateService();
        var createTasks = Enumerable.Range(0, 100)
            .Select(_ => service.CreateAsync(Book.CreateNew()));

        await Task.WhenAll(createTasks);

        var all = await service.ReadAllAsync();
        Assert.Equal(100, all.Count());
    }

    [Fact]
    public async Task SaveAsync_CreatesFile()
    {
        var path = $"test_{Guid.NewGuid()}.json";
        var service = new CrudServiceAsync<Book>(b => b.Id, path);
        await service.CreateAsync(Book.CreateNew());

        var result = await service.SaveAsync();

        Assert.True(result);
        Assert.True(File.Exists(path));
        File.Delete(path);
    }
}
