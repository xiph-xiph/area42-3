using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Area42_3.Models;

public class ReservationEmployee : Reservation
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}