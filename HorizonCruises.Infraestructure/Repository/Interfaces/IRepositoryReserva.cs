using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryReserva
    {
        Task<ICollection<Reserva>> ListAsync();
        Task<ICollection<Reserva>> ListAsyncCliente(int idUsuario);
        Task<Reserva> FindByIdAsync(int id);
        Task<Reserva> CreateAsync(Reserva reserva);
        Task<ICollection<Reserva>> FiltrarPorRangoFechaAsync(DateOnly fechaInicio, DateOnly fechaFinal);
    }
}
