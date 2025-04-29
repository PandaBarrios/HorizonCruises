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
    public class RepositoryCrucero : IRepositoryCrucero
    {

        private readonly HorizonCruisesContext _context;

        public RepositoryCrucero(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<Crucero> CreateAsync(Crucero crucero)
        {
            if (crucero == null)
            {
                throw new ArgumentNullException(nameof(crucero), "El objeto Crucero no puede ser nulo.");
            }

            // Agregar el crucero al contexto y guardar cambios
            var entityEntry = await _context.Set<Crucero>().AddAsync(crucero);
            await _context.SaveChangesAsync();

            // Retornar el crucero con su Id actualizado
            return entityEntry.Entity;
        }


        public async Task<Crucero> FindByIdAsync(int id)
        {
            var crucero = await _context.Set<Crucero>()
                                    .Where(x => x.Id == id)
                                     .Include(x => x.IdBarcoNavigation)
                                        .ThenInclude(x => x.BarcoHabitaciones)
                                        .ThenInclude(x => x.IdHabitacionNavigation)
                                     .Include(x => x.Itinerario)
                                        .ThenInclude(x => x.IdPuertoNavigation)
                                        .ThenInclude(x => x.IdDestinoNavigation)
                                     .Include(x => x.FechaCrucero)
                                        .ThenInclude(x => x.PrecioHabitacion)
                                        .ThenInclude(x => x.IdHabitacionNavigation)
                                     .FirstAsync();

            crucero.Itinerario = crucero.Itinerario.OrderBy(i => i.Orden).ToList();

            return crucero!;
        }

        public async Task<ICollection<Crucero>> ListAsync()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);

            var collection = await _context.Set<Crucero>()
                .Include(x => x.IdBarcoNavigation)
                .Include(x => x.FechaCrucero) 
                .AsNoTracking()
                .Where(c => c.FechaCrucero.Any(f => f.FechaInicio > hoy))
                .ToListAsync();

            return collection;
        }


        public async Task<Crucero> DetalleCrucero(int id)
        {
            var crucero = await _context.Set<Crucero>()
                .Where(x => x.Id == id)
                .Include(x => x.IdBarcoNavigation) // Información del Barco
                    .ThenInclude(b => b.BarcoHabitaciones)
                        .ThenInclude(bh => bh.IdHabitacionNavigation)
                .Include(x => x.Itinerario) // Itinerario
                    .ThenInclude(it => it.IdPuertoNavigation)
                        .ThenInclude(p => p.IdDestinoNavigation) // Para saber de qué destino es el puerto
                .Include(x => x.FechaCrucero) // Fechas del crucero
                    .ThenInclude(fc => fc.PrecioHabitacion) // Precios de habitaciones
                        .ThenInclude(ph => ph.IdHabitacionNavigation) // Información de la habitación
                .FirstOrDefaultAsync();

            if (crucero == null)
            {
                throw new Exception("Crucero no encontrado.");
            }

            // Ordenar el itinerario por orden de visita
            crucero.Itinerario = crucero.Itinerario.OrderBy(i => i.Orden).ToList();

            return crucero;
        }



    }
}
