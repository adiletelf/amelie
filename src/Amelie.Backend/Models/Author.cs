namespace Amelie.Backend.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;

    public IList<Book> Books { get; set; } = new List<Book>();
}
