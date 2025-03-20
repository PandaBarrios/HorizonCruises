using HorizonCruises.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceCrucero
    {
        Task<ICollection<CruceroDTO>> ListAsync();
        Task<CruceroDTO> FindByIdAsync(int id);
        Task<CruceroDTO> CreateAsync(CruceroDTO cruceroDTO);
    }
}
