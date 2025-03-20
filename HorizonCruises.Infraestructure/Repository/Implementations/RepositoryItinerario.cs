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

        public async Task<Itinerario> CreateAsync(Itinerario nItinerario)
        {
            if (nItinerario == null)
            {
                throw new ArgumentNullException(nameof(nItinerario), "El objeto Crucero no puede ser nulo.");
            }

            // Agregar el itinerario
            var entityEntry = await _context.Set<Itinerario>().AddAsync(nItinerario);
            await _context.SaveChangesAsync();

            // Retornar el crucero con su Id actualizado
            return entityEntry.Entity;
        }

        public async Task<Itinerario> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Itinerario>()
                               .Where(x => x.IdCrucero == id)
                               .Include(b => b.IdPuertoNavigation)
                               .ThenInclude(bh => bh.IdDestinoNavigation)
                               .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Itinerario>> ListAsync()
        {
            var collection = await _context.Set<Itinerario>().ToListAsync();
            return collection;
        }
    }
}
