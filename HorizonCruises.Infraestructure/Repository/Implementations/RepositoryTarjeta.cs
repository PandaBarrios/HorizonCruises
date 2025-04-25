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
    public class RepositoryTarjeta : IRepositoryTarjeta
    {

        private readonly HorizonCruisesContext _context;

        public RepositoryTarjeta(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tarjeta>> ListAsync()
        {
            return await _context.Tarjeta.Include(t => t.IdUsuarioNavigation).ToListAsync();
        }

        public async Task<Tarjeta> FindByIdAsync(int id)
        {
            return await _context.Tarjeta
                                 .Include(t => t.IdUsuarioNavigation)
                                 .FirstAsync(t => t.Id == id);
        }

        public async Task<int> AddAsync(Tarjeta entity)
        {
            _context.Tarjeta.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(Tarjeta entity)
        {
            var tarjetaExistente = await _context.Tarjeta.FindAsync(entity.Id);
            if (tarjetaExistente == null)
                throw new KeyNotFoundException($"No se encontró la tarjeta con ID {entity.Id}");

            tarjetaExistente.NumeroTarjeta = entity.NumeroTarjeta;
            tarjetaExistente.FechaCaducidad = entity.FechaCaducidad;
            tarjetaExistente.Cvv = entity.Cvv;
            tarjetaExistente.Titular = entity.Titular;
            tarjetaExistente.IdUsuario = entity.IdUsuario;

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Tarjeta>> GetTarjetasByUsuarioIdAsync(int userId)
        {
            return await _context.Tarjeta
                                 .Where(t => t.IdUsuario == userId)
                                 .ToListAsync();
        }
    }
}
