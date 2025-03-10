using HorizonCruises.Infraestructure.Data;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<int> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }

        //Funcion para buscar Usuarios 
        /*
        public async Task<ICollection<Usuario>> FindByDescriptionAsync(string description)
        {
            description = description.Replace(' ', '%');
            description = "%" + description + "%";
            FormattableString sql = $@"select * from Usuario where Nombre+correo_electronico+fecha_nacimiento+pais like  {description}  ";

            var collection = await _context.Usuario.FromSql(sql).AsNoTracking().ToListAsync();
            return collection;
        } */

        public async Task<Usuario> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Usuario>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
