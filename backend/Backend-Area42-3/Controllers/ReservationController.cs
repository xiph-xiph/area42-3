using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<ActionResult> GetReservations()
    {
        if (User.IsInRole("Employee"))
        {
            var reservations = await _reservationService.GetAllWithUserInfo();
            return Ok(reservations);
        }
        else
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var reservations = await _reservationService.GetByUserId(userId);
            var result = reservations.Select(r => new ReservationDto
            {
                Reservation = r,
                TableName = $"Tafel {r.TableId}"
            }).ToList();
            return Ok(result);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<SuccessMessageDto>> CreateReservation(CreateReservationDto dto)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        dto.UserId = userId;

        var reservation = await _reservationService.Create(dto);

        if (reservation == null)
            return BadRequest(new SuccessMessageDto
            {
                Success = false,
                Message = "Geen beschikbare tafel gevonden voor dit tijdslot."
            });

        return Ok(new SuccessMessageDto
        {
            Success = true,
            Message = "Reservering succesvol aangemaakt."
        });
    }

    [HttpGet("available-slots")]
    [Authorize]
    public async Task<ActionResult<List<DateTime>>> GetAvailableSlots(DateTime date)
    {
        var slots = await _reservationService.GetAvailableSlots(date);
        return Ok(slots);
    }
}