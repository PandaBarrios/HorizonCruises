using HorizonCruises.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceReservaComplementos
    {
        Task CreateAsync(int idReserva, int idComplemento, int cantidad);

    }
}
