namespace Amelie.Backend.Repositories.Exceptions;


[Serializable]
public class BookNotFoundException : Exception
{
	public int BookId { get; set; }
	public BookNotFoundException() { }
	public BookNotFoundException(string message) : base(message) { }
	public BookNotFoundException(string message, Exception inner) : base(message, inner) { }

	public BookNotFoundException(int bookId)
	{
		BookId = bookId;
	}

	public override string ToString()
	{
		return $"The book with bookId: {BookId} was not found.";
	}
}
