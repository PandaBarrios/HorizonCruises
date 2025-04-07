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
        Task<ClienteDTO> FindByIdAsync(string id);
        Task<ClienteDTO> LoginAsync(string id, string password);
        Task<string> AddAsync(ClienteDTO dto);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, ClienteDTO dto);
    }
}
