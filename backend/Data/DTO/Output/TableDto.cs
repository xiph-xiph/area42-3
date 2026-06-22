using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Models;

namespace Backend_Area42_3.DTO.Output;

public class TableDto
{
    public required Table Table { get; set; }
    public required bool IsAvailable { get; set; }
    public required List<DateTime> AvailableSlots { get; set; }
}
