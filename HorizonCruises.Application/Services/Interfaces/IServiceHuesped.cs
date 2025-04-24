using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceHuesped
    {
        Task<ICollection<HuespedDTO>> ListAsync();
        Task<HuespedDTO> AddAsync(HuespedDTO entity);
    }
}
