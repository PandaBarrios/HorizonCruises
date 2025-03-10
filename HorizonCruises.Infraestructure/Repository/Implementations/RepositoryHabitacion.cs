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
    public class RepositoryHabitacion : IRepositoryHabitacion
    {

        private readonly HorizonCruisesContext _context;

        public RepositoryHabitacion(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<Habitacion> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Habitacion>()
                                .Where(x => x.Id == id)
                                .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Habitacion>> ListAsync()
        {
            //Select * from Habitacion 
            var collection = await _context.Set<Habitacion>().ToListAsync();
            return collection;
        }
    }
}
