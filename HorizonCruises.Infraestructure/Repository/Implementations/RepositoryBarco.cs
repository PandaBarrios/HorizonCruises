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
    }
}