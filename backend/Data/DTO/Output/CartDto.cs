using Backend_Area42_3.Models;

namespace Backend_Area42_3.DTO.Output;

public class CartDto : SuccessMessageDto
{
    public required Cart Cart { get; set; }
}
