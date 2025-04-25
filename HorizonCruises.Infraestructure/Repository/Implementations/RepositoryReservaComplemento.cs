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
    public class RepositoryReservaComplemento : IRepositoryReservaComplementos
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryReservaComplemento(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<ReservaComplemento> CreateAsync(ReservaComplemento objeto)
        {
            var entityEntry = await _context.Set<ReservaComplemento>().AddAsync(objeto);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
