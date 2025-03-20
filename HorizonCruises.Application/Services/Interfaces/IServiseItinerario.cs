using HorizonCruises.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiseItinerario
    {
        Task<ICollection<ItinerarioDTO>> ListAsync();
        Task<ItinerarioDTO> FindByIdAsync(int id);
        Task<ItinerarioDTO> CreateAsync(ItinerarioDTO nDTO);
    }
}
