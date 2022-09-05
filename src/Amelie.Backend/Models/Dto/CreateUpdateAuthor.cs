namespace Amelie.Backend.Models.Dto;

public record CreateUpdateAuthor
{
    public string Name { get; set; } = string.Empty;

    public IList<int> BookIds { get; set; } = new List<int>();
}
