using System;
using System.Collections.Generic;
using System.Text;

using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface ITableRepo
{
    Task<List<Table>> GetAll();
    Task<Table?> GetById(int id);
}

