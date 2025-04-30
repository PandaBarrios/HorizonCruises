using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceReserva
    {
        Task<ICollection<ReservaDTO>> ListAsync();
        Task<ICollection<ReservaDTO>> ListAsyncCliente(int idUsuario);
        Task<ReservaDTO> FindByIdAsync(int id);
        Task<ReservaDTO> CreateAsync(ReservaDTO reserva);
        Task<ICollection<ReservaDTO>> FiltrarPorRangoFechaAsync(DateOnly fechaInicio, DateOnly fechaFinal);
    }
}
