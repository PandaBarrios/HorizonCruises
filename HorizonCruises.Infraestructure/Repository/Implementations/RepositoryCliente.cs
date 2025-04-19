using HorizonCruises.Infraestructure.Data;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Implementations
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryCliente(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.CorreoElectronico;
        }

        public async Task DeleteAsync(int id)
        {
            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }

        public async Task<ICollection<Usuario>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                         .Set<Usuario>()
                                         .Where(x => x.Nombre.Contains(description))
                                         .ToListAsync();
            return collection;
        }

        public async Task<Usuario> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Usuario>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>()
                                         .Include(x => x.IdRolNavigation)
                                         .ToListAsync();
            return collection;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            var @object = await _context.Set<Usuario>()
                            .Include(b => b.IdRolNavigation)
                            .Where(p => p.CorreoElectronico == email && p.Contrasena == password)
                            .FirstOrDefaultAsync();

            return @object!; // <-- sin el ! obligatorio

        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
