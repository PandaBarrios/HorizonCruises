using HorizonCruises.Infraestructure.Data;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Implementations
{
    public class RepositoryReservaHabitaciones : IRepositoryReservaHabitacion
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryReservaHabitaciones(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<ReservaHabitacion> CreateAsync(ReservaHabitacion objeto)
        {
            var entityEntry = await _context.Set<ReservaHabitacion>().AddAsync(objeto);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
