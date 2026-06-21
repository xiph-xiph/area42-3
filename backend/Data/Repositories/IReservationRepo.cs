using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IReservationRepo
{
    Task<List<Reservation>> GetAll();
    Task<List<ReservationEmployee>> GetAllWithUserInfo();
    Task<List<Reservation>> GetByUserId(int userId);
    Task<Reservation?> GetById(int id);
    Task Add(Reservation reservation);
}
