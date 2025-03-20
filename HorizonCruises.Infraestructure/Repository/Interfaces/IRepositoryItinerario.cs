using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryItinerario
    {
        Task<ICollection<Itinerario>> ListAsync();
        Task<Itinerario> FindByIdAsync(int id);
        Task<Itinerario> CreateAsync(Itinerario nItinerario);
    }
}
