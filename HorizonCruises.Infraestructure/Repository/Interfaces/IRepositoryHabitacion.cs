﻿using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryHabitacion
    {
        Task<ICollection<Habitacion>> ListAsync();
        Task<Habitacion> FindByIdAsync(int id);
        Task<int> AddAsync(Habitacion entity);
        Task UpdateAsync(Habitacion entity);
    }
}
