using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPrecioHabitacion
    {
        Task<ICollection<PrecioHabitacion>> ListAsync();
        Task<PrecioHabitacion> FindByIdAsync(int id);
        Task<PrecioHabitacion> CreateAsync(PrecioHabitacion nPrecio);
    }
}
