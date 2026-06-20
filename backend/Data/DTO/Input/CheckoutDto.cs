namespace Backend_Area42_3.DTO.Input;

public class CheckoutDto
{
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required DateTime? PickupTime { get; set; }
    public required string? Remarks { get; set; }
}