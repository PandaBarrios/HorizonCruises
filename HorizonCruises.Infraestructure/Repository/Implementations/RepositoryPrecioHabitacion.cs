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
    public class RepositoryPrecioHabitacion : IRepositoryPrecioHabitacion
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryPrecioHabitacion(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<PrecioHabitacion> CreateAsync(PrecioHabitacion nPrecio)
        {
            if (nPrecio == null)
            {
                throw new ArgumentNullException(nameof(nPrecio), "El objeto Crucero no puede ser nulo.");
            }

            // Agregar el itinerario
            var entityEntry = await _context.Set<PrecioHabitacion>().AddAsync(nPrecio);
            await _context.SaveChangesAsync();

            // Retornar el crucero con su Id actualizado
            return entityEntry.Entity;
        }

        public async Task<PrecioHabitacion> FindByIdAsync(int id)
        {
            var @object = await _context.Set<PrecioHabitacion>()
                               .Where(x => x.Id == id)
                               .Include(b => b.IdHabitacionNavigation)
                               .Include(b => b.IdCruceroFechaNavigation)
                               .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<PrecioHabitacion>> ListAsync()
        {
            var collection = await _context.Set<PrecioHabitacion>().ToListAsync();
            return collection;
        }
    }
}
