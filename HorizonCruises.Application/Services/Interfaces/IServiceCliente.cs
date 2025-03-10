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
        Task<ICollection<ClienteDTO>> ListAsync();
        Task<ClienteDTO> FindByIdAsync(int id);
    }
}
