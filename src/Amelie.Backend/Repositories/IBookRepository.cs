using Amelie.Backend.Models;

namespace Amelie.Backend.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> Create(Book book);
    Task<Book?> Update(Book book);
    Task Remove(Book book);
    Task<bool> Exists(int id);
    Task AddAuthorsAsync(int bookId, IEnumerable<int> authorIds);
    Task RemoveAuthorsAsync(int bookId, IEnumerable<int> authorIds);
}
