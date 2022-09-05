namespace Amelie.Backend.Repositories.Exceptions;

[Serializable]
public class AuthorNotFoundException : Exception
{
    public int AuthorId { get; set; }

    public AuthorNotFoundException() { }

    public AuthorNotFoundException(string message)
        : base(message) { }

    public AuthorNotFoundException(string message, Exception inner)
        : base(message, inner) { }

    public AuthorNotFoundException(int authorId)
    {
        AuthorId = authorId;
    }

    public override string ToString()
    {
        return $"The author with authorId: {AuthorId} was not found.";
    }
}
