using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceBarcoHabitaciones
    {
        Task<ICollection<BarcoHabitacionesDTO>> ListAsync();
        Task<BarcoHabitacionesDTO> FindByIdAsync(int id);
        Task<ICollection<BarcoHabitacionesDTO>> GetHabitacionesByBarcoAsync(int idBarco);
        Task UpdateAsync(int idBarco, int idHabitacion, int disponibles);
        Task<BarcoHabitacionesDTO> GetHabitacionPorId(int id);
    }
}
