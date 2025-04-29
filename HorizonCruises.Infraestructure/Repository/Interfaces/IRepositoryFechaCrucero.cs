using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryFechaCrucero
    {
        Task<ICollection<FechaCrucero>> ListAsync();
        Task<FechaCrucero> FindByIdAsync(int id);
        Task<FechaCrucero> CreateAsync(FechaCrucero nFecha);
        Task<PrecioHabitacion> CreatePrecioHabitacionAsync(PrecioHabitacion precioHabitacionDTO);

    }
}
