namespace Backend_Area42_3.Models;

public class MenuItem
{
    public required int Id { get; set; }

    public required decimal Price { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}
