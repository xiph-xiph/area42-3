using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Backend_Area42_3.DTO.Input;

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

    public async Task<Reservation?> Create(CreateReservationDto dto)
    {
        // Check 1: bestaat de gebruiker?
        var user = await _userRepository.GetUserById(dto.UserId);
        if (user == null) return null;

        // Check 2: past het aantal gasten op de tafel?
        var tafel = await _tableRepo.GetById(dto.TableId);
        if (tafel == null) return null;
        if (dto.Amount > tafel.MaxGuests) return null;

        // Check 3: is het tijdslot beschikbaar?
        var isAvailable = await CheckAvailability(dto.TableId, dto.StartDate);
        if (!isAvailable) return null;

        // Alles goed, maak reservering aan
        var reservation = new Reservation
        {
            UserId = dto.UserId,
            TableId = dto.TableId,
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

    public async Task<List<DateTime>> GetAvailableSlots(int tableId, DateTime date)
    {
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
            if (await CheckAvailability(tableId, slot))
            {
                availableSlots.Add(slot);
            }
        }

        return availableSlots;
    }
}
