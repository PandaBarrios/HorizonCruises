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
    public class RepositoryBarcoHabitaciones : IRepositoryBarcoHabitaciones
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryBarcoHabitaciones(HorizonCruisesContext context)
        {
            _context = context;
        }

        // Obtener una relación barco-habitación específica por ID (si es necesario)
        public async Task<BarcoHabitaciones> FindByIdAsync(int id)
        {
            return await _context.BarcoHabitaciones
                .Include(bh => bh.IdBarcoNavigation)
                .Include(bh => bh.IdHabitacionNavigation)
                .FirstAsync(bh => bh.IdBarco == id);
        }

        // Listar TODAS las relaciones barco-habitaciones
        public async Task<ICollection<BarcoHabitaciones>> ListAsync()
        {
            return await _context.BarcoHabitaciones
                .Include(bh => bh.IdBarcoNavigation)
                .Include(bh => bh.IdHabitacionNavigation)
                .ToListAsync();
        }

        // Método para obtener las habitaciones de un barco específico
        public async Task<ICollection<BarcoHabitaciones>> GetHabitacionesByBarcoAsync(int idBarco)
        {
            return await _context.BarcoHabitaciones
                .Where(bh => bh.IdBarco == idBarco)
                .Include(bh => bh.IdHabitacionNavigation)
                    .ThenInclude(h => h.PrecioHabitacion)
                .OrderBy(bh => bh.IdHabitacionNavigation.Nombre)
                .ToListAsync();
        }

        public async Task UpdateAsync(int idBarco, int idHabitacion, int cantidadARestar)
        {
            var entity = await _context.BarcoHabitaciones
                .FirstOrDefaultAsync(bh => bh.IdBarco == idBarco && bh.IdHabitacion == idHabitacion);

            if (entity == null)
                throw new Exception("Relación Barco-Habitación no encontrada.");

            if (entity.TotalHabitacionesDisponibles < cantidadARestar)
                throw new InvalidOperationException("No hay suficientes habitaciones disponibles para restar.");

            entity.TotalHabitacionesDisponibles -= cantidadARestar;
            await _context.SaveChangesAsync();
        }


        public async Task<BarcoHabitaciones> GetHabitacionPorId(int id)
        {
            return await _context.BarcoHabitaciones
                          .Include(b => b.IdHabitacionNavigation)
                          .ThenInclude(h => h.PrecioHabitacion)
                          .FirstOrDefaultAsync(b => b.IdHabitacion == id);

        }

        public async Task<ICollection<Habitacion>> HabitacionesPorBarcoAsync(int idBarco)
        {
            return await _context.BarcoHabitaciones
                    .Where(bh => bh.IdBarco == idBarco)
                    .Include(bh => bh.IdHabitacionNavigation)
                    .Select(bh => bh.IdHabitacionNavigation) 
                    .ToListAsync();
        }

        public async Task RemoveByBarcoIdAsync(int barcoId)
        {
            var habitaciones = await _context.BarcoHabitaciones
                .Where(bh => bh.IdBarco == barcoId)
                .ToListAsync();

            _context.BarcoHabitaciones.RemoveRange(habitaciones);
            await _context.SaveChangesAsync();
        }
    }
}
