using Amelie.Backend.Data;
using Amelie.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Amelie.Backend.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> GetAll()
    {
        return QueryBooks();
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        return QueryBooks().SingleOrDefaultAsync(b => b.BookId == id);
    }

    public async Task<Book> Create(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> Update(Book book)
    {
        if (!_context.Books.Any(b => b.BookId == book.BookId))
        {
            return null;
        }

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task Remove(Book book)
    {
        _context.Remove(book);
        await _context.SaveChangesAsync();
    }

    private IQueryable<Book> QueryBooks()
    {
        return _context.Books.Select(b => new Book
        {
            BookId = b.BookId,
            Title = b.Title,
            Publisher = b.Publisher,
            Description = b.Description,
            Authors = b.Authors.AsQueryable().Select(a => new Author
            {
                AuthorId = a.AuthorId,
                Name = a.Name
            }).ToList(),
        });
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Books.AnyAsync(a => a.BookId == id);
    }

    public async Task AddAuthorsAsync(int bookId, IEnumerable<int> authorIds)
    {
        var book = await _context.Books
            .Include(a => a.Authors)
            .SingleAsync(a => a.BookId == bookId);

        var authors = _context.Authors.Where(a => authorIds.Contains(a.AuthorId));
        foreach (Author author in authors)
        {
            book.Authors.Add(author);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAuthorsAsync(int bookId, IEnumerable<int> authorIds)
    {
        var book = await _context.Books
            .Include(a => a.Authors)
            .SingleAsync(a => a.BookId == bookId);

        var authors = _context.Authors.Where(a => authorIds.Contains(a.AuthorId));
        foreach (Author author in authors)
        {
            book.Authors.Remove(author);
        }

        await _context.SaveChangesAsync();
    }
}
