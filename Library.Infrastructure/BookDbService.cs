using System.Collections;
using Library.Common;
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

// CrudService через Repository для BookModel
public class BookDbService : Library.Common.ICrudServiceAsync<BookModel>
{
    private readonly IRepository<BookModel> _repository;
    private readonly LibraryContext _context;

    public BookDbService(LibraryContext context)
    {
        _context = context;
        _repository = new Repository<BookModel>(context);
    }

    public async Task<bool> CreateAsync(BookModel element)
    {
        await _repository.AddAsync(element);
        return true;
    }

    public async Task<BookModel> ReadAsync(Guid id)
    {
        var all = await _context.Books.Where(b => b.Guid == id).FirstOrDefaultAsync();
        return all ?? throw new KeyNotFoundException();
    }

    public async Task<IEnumerable<BookModel>> ReadAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<IEnumerable<BookModel>> ReadAllAsync(int page, int amount)
    {
        return await _context.Books
            .Skip((page - 1) * amount)
            .Take(amount)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(BookModel element)
    {
        await _repository.Update(element);
        return true;
    }

    public async Task<bool> RemoveAsync(BookModel element)
    {
        await _repository.Delete(element);
        return true;
    }

    public async Task<bool> SaveAsync()
    {
        await _context.SaveChangesAsync();
        return true;
    }

    public IEnumerator<BookModel> GetEnumerator() =>
        _context.Books.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
