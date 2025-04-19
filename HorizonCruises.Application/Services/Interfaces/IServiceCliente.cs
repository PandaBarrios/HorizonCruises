using HorizonCruises.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceCliente
    {
        Task<ICollection<ClienteDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<ClienteDTO>> ListAsync();
        Task<ClienteDTO> FindByIdAsync(int id);
        Task<ClienteDTO> LoginAsync(string email, string password);
        Task<string> AddAsync(ClienteDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ClienteDTO dto);
    }
}
