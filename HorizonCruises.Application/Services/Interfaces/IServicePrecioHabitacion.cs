using HorizonCruises.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServicePrecioHabitacion
    {
        Task<ICollection<PrecioHabitacionDTO>> ListAsync();
        Task<PrecioHabitacionDTO> FindByIdAsync(int id);
        Task<PrecioHabitacionDTO> CreateAsync(PrecioHabitacionDTO nPrecio);
    }
}
