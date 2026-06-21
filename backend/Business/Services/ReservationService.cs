using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Services;

public class ReservationService(
    IReservationRepo reservationRepo,
    ITableRepo tableRepo,
    IUserRepository userRepository)
{
    private readonly IReservationRepo _reservationRepo = reservationRepo;
    private readonly ITableRepo _tableRepo = tableRepo;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<List<Reservation>> GetAll()
    {
        return await _reservationRepo.GetAll();
    }

    public async Task<List<Reservation>> GetByUserId(int userId)
    {
        return await _reservationRepo.GetByUserId(userId);
    }

    public async Task<Reservation?> Create(CreateReservationDto dto)
    {
        // Check 1: bestaat de gebruiker?
        var user = await _userRepository.GetUserById(dto.UserId);
        if (user == null) return null;

        // Check 2: zoek automatisch een beschikbare tafel
        var tables = await _tableRepo.GetAll();
        Table? beschikbareTafel = null;

        foreach (var tafel in tables)
        {
            if (tafel.MaxGuests >= dto.Amount)
            {
                var isAvailable = await CheckAvailability(tafel.Id, dto.StartDate);
                if (isAvailable)
                {
                    beschikbareTafel = tafel;
                    break;
                }
            }
        }

        if (beschikbareTafel == null) return null;

        var reservation = new Reservation
        {
            UserId = dto.UserId,
            TableId = beschikbareTafel.Id,
            StartDate = dto.StartDate,
            Amount = dto.Amount,
            Restaurant = Restaurant.Restaurant,
            Status = ReservationStatus.Scheduled
        };

        await _reservationRepo.Add(reservation);
        return reservation;
    }

    public async Task<bool> CheckAvailability(int tableId, DateTime date)
    {
        var reservations = await _reservationRepo.GetAll();
        return !reservations.Any(r =>
            r.TableId == tableId &&
            r.StartDate == date &&
            r.Status == ReservationStatus.Scheduled);
    }

    public async Task<List<DateTime>> GetAvailableSlots(DateTime date)
    {
        var tables = await _tableRepo.GetAll();
        var slots = new List<DateTime>
        {
            date.Date.AddHours(17),
            date.Date.AddHours(18),
            date.Date.AddHours(19),
            date.Date.AddHours(20),
            date.Date.AddHours(21)
        };

        var availableSlots = new List<DateTime>();
        foreach (var slot in slots)
        {
            foreach (var tafel in tables)
            {
                if (await CheckAvailability(tafel.Id, slot))
                {
                    availableSlots.Add(slot);
                    break;
                }
            }
        }

        return availableSlots.Distinct().ToList();
    }
}