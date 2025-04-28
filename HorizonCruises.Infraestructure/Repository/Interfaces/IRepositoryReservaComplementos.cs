using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryReservaComplementos
    {
        Task<ReservaComplemento> CreateAsync(ReservaComplemento objeto);
    }
}
