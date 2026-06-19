using Backend_Area42_3.Models;

namespace Backend_Area42_3.DTO.Output;

public class OrderListDto : SuccessMessageDto
{
    public required List<Order> Orders { get; set; }
}