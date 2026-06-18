
using Microsoft.AspNetCore.Mvc;
using Backend_Area42_3.Services;
using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Models;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<List<ReservationDto>> GetReservations()
    {
        var reservations = _reservationService.GetAll();
        return Ok(reservations);
    }

    [HttpPost]
    public ActionResult<ReservationDto> CreateReservation(CreateReservationDto dto)
    {
        var isAvailable = _reservationService.CheckAvailability(dto.TableId, dto.StartDate);

        if (!isAvailable)
        {
            return BadRequest(new { message = "Dit tijdslot is niet beschikbaar." });
        }

        var reservation = _reservationService.Create(dto);

        if (reservation == null)
        {
            return BadRequest(new { message = "Reservering kon niet aangemaakt worden." });
        }

        return Ok(reservation);
    }

    [HttpGet("available-slots")]
    public ActionResult<List<DateTime>> GetAvailableSlots(int tableId, DateTime date)
    {
        var slots = _reservationService.GetAvailableSlots(tableId, date);
        return Ok(slots);
    }
}