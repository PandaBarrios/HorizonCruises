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
    public class RepositoryComplemento : IRepositoryComplemento
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryComplemento(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Complemento entity)
        {
            _context.Complemento.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Complemento> FindByIdAsync(int id)
        {
            var complemento = await _context.Set<Complemento>()
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
            return complemento!;
        }

        public async Task<ICollection<Complemento>> ListAsync()
        {
            var collection = await _context.Set<Complemento>()
                                    .OrderBy(x => x.Nombre)
                                    .ToListAsync();
            return collection;
        }

        public async Task UpdateAsync(Complemento entity)
        {
            _context.Complemento.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
