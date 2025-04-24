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
    public class RepositoryUsuarioHuesped : IRepositoryUsuarioHuesped
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryUsuarioHuesped(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<UsuarioHuesped> AddAsync(UsuarioHuesped entity)
        {
            var entityEntry = await _context.Set<UsuarioHuesped>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<ICollection<UsuarioHuesped>> GetHuespedByUsuarioAsync(int idUsuario)
        {
            return await _context.UsuarioHuesped
                         .Include(uh => uh.IdHuespedNavigation)
                         .Where(uh => uh.IdUsuario == idUsuario)
                         .ToListAsync();

        }
    }
}
