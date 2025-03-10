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
    public class RepositoryRol: IRepositoryRol
    {

        private readonly HorizonCruisesContext _context;

        public RepositoryRol(HorizonCruisesContext context)
        {
            _context = context;
        }

        public Task<Rol> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Rol>> ListAsync()
        {
            //Select * from Rol 
            var collection = await _context.Set<Rol>().ToListAsync();
            return collection;
        }
    }
}
