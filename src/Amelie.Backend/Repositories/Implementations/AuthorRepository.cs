using Amelie.Backend.Data;
using Amelie.Backend.Models;
using Amelie.Backend.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Amelie.Backend.Repositories.Implementations;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;
    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Author> GetAll()
    {
        return QueryAuthors();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await QueryAuthors().SingleOrDefaultAsync(a => a.AuthorId == id);
    }

    public async Task<Author> Create(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<Author?> Update(Author author)
    {
        if (!_context.Authors.Any(a => a.AuthorId == author.AuthorId))
        {
            return null;
        }

        _context.Entry(author).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task Remove(Author author)
    {
        _context.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Authors.AnyAsync(a => a.AuthorId == id);
    }

    public async Task AddBooks(int authorId, IEnumerable<int> bookIds)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .SingleAsync(a => a.AuthorId == authorId);

        var books = _context.Books.Where(b => bookIds.Contains(b.BookId));
        foreach (Book book in books)
        {
            author.Books.Add(book);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveBooks(int authorId, IEnumerable<int> bookIds)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .SingleAsync(a => a.AuthorId == authorId);

        var books = _context.Books.Where(b => bookIds.Contains(b.BookId));
        foreach (Book book in books)
        {
            author.Books.Remove(book);
        }

        await _context.SaveChangesAsync();
    }

    private IQueryable<Author> QueryAuthors()
    {
        return _context.Authors.Select(a => new Author
        {
            AuthorId = a.AuthorId,
            Name = a.Name,
            Books = a.Books.AsQueryable().Select(b => new Book
            {
                BookId = b.BookId,
                Title = b.Title,
                Publisher = b.Publisher,
                Description = b.Description,
            }).ToList(),
        });
    }
}
