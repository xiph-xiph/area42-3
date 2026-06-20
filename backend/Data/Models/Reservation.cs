using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Models;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TafelId { get; set; }
    public DateTime StartDate { get; set; }
    public int Amount { get; set; }
    public Restaurant Restaurant { get; set; }
    public ReservationStatus Status { get; set; }
}
