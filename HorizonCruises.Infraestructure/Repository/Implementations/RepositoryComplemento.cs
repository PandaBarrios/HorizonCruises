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

        public async Task<Complemento> FindByIdAsync(int id)
        {
            var complemento = await _context.Complemento.FirstOrDefaultAsync(x => x.Id == id);

            if (complemento == null)
                throw new InvalidOperationException($"No se encontró ningún complemento con ID: {id}");

            return complemento;
        }

        public async Task<ICollection<Complemento>> ListAsync()
        {
            var collection = await _context.Set<Complemento>().ToListAsync();
            return collection;
        }
    }
}
