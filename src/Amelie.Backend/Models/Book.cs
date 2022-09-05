namespace Amelie.Backend.Models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string? Description { get; set; }

    public IList<Author> Authors { get; set; } = new List<Author>();
}
