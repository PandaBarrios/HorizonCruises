using HorizonCruises.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServicePuerto
    {
        Task<ICollection<PuertoDTO>> ListAsync();
        Task<PuertoDTO> FindByIdAsync(int id);
        Task<ICollection<PuertoDTO>> FindByNameAsync(string nombre);


    }
}
