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
    public class RepositoryPuerto : IRepositoryPuerto
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryPuerto(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<Puerto> FindByIdAsync(int id)
        {
            //Obtener un Libro con su autor y las lista de categorías
            var @object = await _context.Set<Puerto>()
                                .Where(x => x.Id == id)
                                .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Puerto>> ListAsync()
        {
            //Select * from Puerto 
            var collection = await _context.Set<Puerto>().ToListAsync();
            return collection;
        }
    }
}
