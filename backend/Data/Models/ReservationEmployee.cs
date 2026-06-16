using Backend_Area42_3.Enums;
namespace Backend_Area42_3.Models;

public class ReservationEmployee : Reservation
{
    public int Phone { get; set; }
    public string Email { get; set; } = "";
}
