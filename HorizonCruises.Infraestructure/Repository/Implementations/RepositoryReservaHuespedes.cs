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
    public class RepositoryReservaHuespedes : IRepositoryReservaHuespedes
    {

        private readonly HorizonCruisesContext _context;

        public RepositoryReservaHuespedes(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<ReservaHuesped> CreateAsync(ReservaHuesped objeto)
        {
            var entityEntry = await _context.Set<ReservaHuesped>().AddAsync(objeto);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
