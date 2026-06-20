using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Area42_3.DTO.Input;

public class CreateReservationDto
{
    public required int UserId { get; set; }
    public required int TableId { get; set; }
    public required DateTime StartDate { get; set; }
    public required int Amount { get; set; }
}
