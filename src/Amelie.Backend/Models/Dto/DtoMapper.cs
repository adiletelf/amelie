namespace Amelie.Backend.Models.Dto;

public static class DtoMapper
{
    public static Author FromDto(this CreateUpdateAuthor dto)
    {
        return new Author
        {
            Name = dto.Name,
            Books = dto.BookIds.Select(x => new Book { BookId = x }).ToList(),
        };
    }

    public static Book FromDto(this CreateUpdateBook dto)
    {
        return new Book
        {
            Title = dto.Title,
            Publisher = dto.Publisher,
            Description = dto.Description,
            Authors = dto.AuthorIds.Select(x => new Author { AuthorId = x }).ToList(),
        };
    }
}
