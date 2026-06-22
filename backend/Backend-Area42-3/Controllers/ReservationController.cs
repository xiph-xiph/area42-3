using Microsoft.AspNetCore.Mvc;
using Backend_Area42_3.Services;
using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(ReservationService reservationService) : ControllerBase
{
    private readonly ReservationService _reservationService = reservationService;

    [HttpGet]
    public async Task<ActionResult<List<ReservationDto>>> GetReservations()
    {
        var reservations = await _reservationService.GetAll();
        var result = reservations.Select(r => new ReservationDto
        {
            Reservation = r,
            TableName = $"Tafel {r.TableId}"
        }).ToList();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateReservation(CreateReservationDto dto)
    {
        var isAvailable = await _reservationService.CheckAvailability(dto.TableId, dto.StartDate);
        if (!isAvailable)
            return BadRequest(new { message = "Dit tijdslot is niet beschikbaar." });

        var reservation = await _reservationService.Create(dto);
        if (reservation == null)
            return BadRequest(new { message = "Reservering kon niet aangemaakt worden." });

        return Ok(new ReservationDto { Reservation = reservation, TableName = $"Tafel {reservation.TableId}" });
    }

    [HttpGet("available-slots")]
    public async Task<ActionResult<List<DateTime>>> GetAvailableSlots(int tableId, DateTime date)
    {
        var slots = await _reservationService.GetAvailableSlots(tableId, date);
        return Ok(slots);
    }
}
