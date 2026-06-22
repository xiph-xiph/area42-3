using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Models;

public class Issue
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required PriorityEnum Priority { get; set; }
    public required CategoryEnum Category { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreationDate{ get; set; }
    public DateTime? SolvedDate { get; set; }
    public required bool Solved { get; set; }

}
