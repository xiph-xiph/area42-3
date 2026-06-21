using Microsoft.AspNetCore.Mvc;
using Backend_Area42_3.Services;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController(TableService tableService, ReservationService reservationService) : ControllerBase
{
    private readonly TableService _tableService = tableService;
    private readonly ReservationService _reservationService = reservationService;

    [HttpGet]
    public async Task<ActionResult<List<TableDto>>> GetTables(DateTime date)
    {
        var tables = await _tableService.GetAll();
        var result = new List<TableDto>();

        foreach (var t in tables)
        {
            result.Add(new TableDto
            {
                Table = t,
                IsAvailable = await _reservationService.CheckAvailability(t.Id, date),
                AvailableSlots = await _reservationService.GetAvailableSlots(date)
            });
        }

        return Ok(result);
    }
}
