using Backend_Area42_3.Models;

namespace Backend_Area42_3.DTO.Output;

public class MenuDto : SuccessMessageDto
{
    public required List<MenuItem> Menu { get; set; }
}
