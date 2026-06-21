namespace Backend_Area42_3.Models;

public class MenuItem
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public required string Category { get; set; }
    public required bool IsPopular { get; set; }
}
