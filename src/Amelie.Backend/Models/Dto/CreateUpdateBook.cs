namespace Amelie.Backend.Models.Dto;

public record CreateUpdateBook
{
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string? Description { get; set; }

    public IList<int> AuthorIds { get; set; } = new List<int>();
}
