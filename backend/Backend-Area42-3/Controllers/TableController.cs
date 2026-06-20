
using Microsoft.AspNetCore.Mvc;
using Backend_Area42_3.Services;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly TableService _tableService;
    private readonly ReservationService _reservationService;

    public TableController(TableService tableService, ReservationService reservationService)
    {
        _tableService = tableService;
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<List<TableDto>> GetTables(DateTime date)
    {
        var tables = _tableService.GetAll();

        var result = tables.Select(t => new TableDto
        {
            Table = t,
            IsAvailable = _reservationService.CheckAvailability(t.Id, date),
            AvailableSlots = _reservationService.GetAvailableSlots(t.Id, date)
        }).ToList();

        return Ok(result);
    }
}