using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryBarcoHabitaciones
    {
        Task<ICollection<BarcoHabitaciones>> ListAsync();
        Task<BarcoHabitaciones> FindByIdAsync(int id);
        Task<ICollection<BarcoHabitaciones>> GetHabitacionesByBarcoAsync(int idBarco);
        Task UpdateAsync(int idBarco, int idHabitacion, int disponibles);
        Task<BarcoHabitaciones> GetHabitacionPorId(int id);
        Task RemoveByBarcoIdAsync(int barcoId);


        Task<ICollection<Habitacion>> HabitacionesPorBarcoAsync(int idBarco);

    }
}
