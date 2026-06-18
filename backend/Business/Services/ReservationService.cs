
using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;
using Backend_Area42_3.DTO.Input;

namespace Backend_Area42_3.Services;

public class ReservationService
{
    private readonly IReservationRepo _reservationRepo;
    private readonly ITableRepo _tableRepo;
    private readonly IUserRepo _userRepo;

    public ReservationService(IReservationRepo reservationRepo, ITableRepo tableRepo, IUserRepo userRepo)
    {
        _reservationRepo = reservationRepo;
        _tableRepo = tableRepo;
        _userRepo = userRepo;
    }

    public List<Reservation> GetAll()
    {
        throw new NotImplementedException();
    }

    public Reservation? Create(CreateReservationDto dto)
    {
        throw new NotImplementedException();
    }

    public bool CheckAvailability(int tableId, DateTime date)
    {
        throw new NotImplementedException();
    }

    public List<DateTime> GetAvailableSlots(int tableId, DateTime date)
    {
        throw new NotImplementedException();
    }
}