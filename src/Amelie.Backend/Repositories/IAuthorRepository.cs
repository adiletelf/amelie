using Amelie.Backend.Models;

namespace Amelie.Backend.Repositories;

public interface IAuthorRepository
{
    IEnumerable<Author> GetAll();
    Task<Author?> GetByIdAsync(int id);
    Task<Author> Create(Author author);
    Task<Author?> Update(Author author);
    Task Remove(Author author);
    Task<bool> Exists(int id);
    Task AddBooks(int authorId, IEnumerable<int> bookIds);
    Task RemoveBooks(int authorId, IEnumerable<int> bookIds);
}
