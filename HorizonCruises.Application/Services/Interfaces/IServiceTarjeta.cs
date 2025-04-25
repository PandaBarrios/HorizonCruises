using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceTarjeta
    {
        Task<ICollection<TarjetaDTO>> ListAsync();
        Task<TarjetaDTO> FindByIdAsync(int id);
        Task<int> AddAsync(TarjetaDTO dto);
        Task UpdateAsync(int id, TarjetaDTO dto);
        Task<ICollection<TarjetaDTO>> GetTarjetasByUsuarioIdAsync(int userId);
    }
}
