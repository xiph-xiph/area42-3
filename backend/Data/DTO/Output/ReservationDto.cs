using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Models;

namespace Backend_Area42_3.DTO.Output;

public class ReservationDto
{
    public required Reservation Reservation { get; set; }
    public required string TableName { get; set; }
}
