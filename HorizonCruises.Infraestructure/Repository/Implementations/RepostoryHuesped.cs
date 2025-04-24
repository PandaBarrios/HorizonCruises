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
    public class RepostoryHuesped : IRepositoryHuesped
    {

        private readonly HorizonCruisesContext _context;

        public RepostoryHuesped(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<Huesped> AddAsync(Huesped entity)
        {
            await _context.Set<Huesped>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<Huesped>> ListAsync()
        {
            var collection = await _context.Set<Huesped>().ToListAsync();
            return collection;
        }
    }
}
