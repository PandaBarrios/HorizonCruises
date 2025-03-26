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
                .ToListAsync();
        }
    }
}
