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
    public class RepositoryFechaCrucero : IRepositoryFechaCrucero
    {
        private readonly HorizonCruisesContext _context;
        public RepositoryFechaCrucero(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<FechaCrucero> CreateAsync(FechaCrucero nFecha)
        {
            if (nFecha == null)
            {
                throw new ArgumentNullException(nameof(nFecha), "El objeto Crucero no puede ser nulo.");
            }

            // Agregar el crucero al contexto y guardar cambios
            var entityEntry = await _context.Set<FechaCrucero>().AddAsync(nFecha);
            await _context.SaveChangesAsync();

            // Retornar el crucero con su Id actualizado
            return entityEntry.Entity;
        }

        public async Task<PrecioHabitacion> CreatePrecioHabitacionAsync(PrecioHabitacion precioHabitacion)
        {
            if (precioHabitacion == null)
            {
                throw new ArgumentNullException(nameof(precioHabitacion), "El precio de habitación no puede ser nulo.");
            }

            var entityEntry = await _context.Set<PrecioHabitacion>().AddAsync(precioHabitacion);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }


        public async Task<FechaCrucero> FindByIdAsync(int id)
        {
            var @object = await _context.Set<FechaCrucero>()
                                 .Where(x => x.Id == id)
                                 .Include(b => b.IdCruceroNavigation) 
                                 .ThenInclude(bh => bh.IdBarcoNavigation)
                                 .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<FechaCrucero>> ListAsync()
        { 
            var collection = await _context.Set<FechaCrucero>().ToListAsync();
            return collection;
        }
    }
}
