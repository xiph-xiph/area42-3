using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Models;

public class Table
{
    public int Id { get; set; }
    public Restaurant Restaurant { get; set; }
    public int MaxGuests { get; set; }
}
