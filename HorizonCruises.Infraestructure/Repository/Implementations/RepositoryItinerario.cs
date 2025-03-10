using HorizonCruises.Infraestructure.Data;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Implementations
{
    public class RepositoryItinerario : IRepositoryItinerario
    {
        private readonly HorizonCruisesContext _context;
        public RepositoryItinerario(HorizonCruisesContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Itinerario>> CruceroItinerario(int id)
        {
            return await _context.Itinerario
                                 .Where(i => i.IdCrucero == id)
                                 .ToListAsync();
        }
    }
}
