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
    public class RepositoryBarco : IRepositoryBarco
    {

        private readonly HorizonCruisesContext _context;

        public RepositoryBarco(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Barco>> ListAsync()
        {
            //Select * from Barco 
            var collection = await _context.Set<Barco>().ToListAsync();
            return collection;
        }

        public async Task<Barco> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Barco>()
                                .Where(x => x.Id == id)
                                .Include(b => b.BarcoHabitaciones) // Incluir la relación con las habitaciones
                                .ThenInclude(bh => bh.IdHabitacionNavigation) // Incluir los detalles de la habitación
                                .FirstAsync();
            return @object!;
        }

        public async Task<Barco?> ObtenerBarcoPorIdAsync(int id)
        {
            return await _context.Barco
                .Include(b => b.BarcoHabitaciones)
                .ThenInclude(bh => bh.IdHabitacionNavigation)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> ExisteHabitacionEnBarco(int idBarco, int idHabitacion)
        {
            return await _context.BarcoHabitaciones
                .AnyAsync(bh => bh.IdBarco == idBarco && bh.IdHabitacion == idHabitacion);
        }

        public async Task<bool> UpdateAsync(Barco entity)
        {
            _context.Barco.Update(entity);
            return await _context.SaveChangesAsync() > 0; 
        }

        public async Task<bool> AddAsync(Barco entity)
        {
            _context.Barco.Add(entity);
            return await _context.SaveChangesAsync() > 0; 
        }

        public void RemoveHabitacionesByBarcoId(int barcoId)
        {
            var habitaciones = _context.BarcoHabitaciones.Where(bh => bh.IdBarco == barcoId);
            _context.BarcoHabitaciones.RemoveRange(habitaciones);
            _context.SaveChanges();
        }
    }
}