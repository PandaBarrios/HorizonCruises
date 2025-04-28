using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryReservaHabitacion
    {
        Task<ReservaHabitacion> CreateAsync(ReservaHabitacion objeto);
    }
}
