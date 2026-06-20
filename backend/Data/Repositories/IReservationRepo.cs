using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IReservationRepo
{
    Task<List<Reservation>> GetAll();
    Task<Reservation?> GetById(int id);
    Task Add(Reservation reservation);
}
