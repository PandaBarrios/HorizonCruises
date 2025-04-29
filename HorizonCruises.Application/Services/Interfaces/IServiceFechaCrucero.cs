using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceFechaCrucero
    {
        Task<ICollection<FechaCruceroDTO>> ListAsync();
        Task<FechaCruceroDTO> FindByIdAsync(int id);
        Task<FechaCruceroDTO> CreateAsync(FechaCruceroDTO FechaDTO);
        Task<PrecioHabitacionDTO> CreatePrecioHabitacionAsync(PrecioHabitacionDTO precioHabitacion);
    }
}
