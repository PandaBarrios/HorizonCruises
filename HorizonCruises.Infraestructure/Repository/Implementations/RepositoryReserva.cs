﻿using HorizonCruises.Infraestructure.Data;
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
    public class RepositoryReserva : IRepositoryReserva
    {
        private readonly HorizonCruisesContext _context;

        public RepositoryReserva(HorizonCruisesContext context)
        {
            _context = context;
        }

        public async Task<Reserva> CreateAsync(Reserva reserva)
        {
            var entityEntry = await _context.Set<Reserva>().AddAsync(reserva);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<ICollection<Reserva>> FiltrarPorRangoFechaAsync(DateOnly fechaInicio, DateOnly fechaFinal)
        {
            var reservas = await _context.Reserva
                .Where(r => r.FechaReserva >= fechaInicio && r.FechaReserva <= fechaFinal)
                .Include(r => r.IdUsuarioNavigation)
                .Include(r => r.IdCruceroNavigation)
                .Include(r => r.ReservaHabitacion)
                    .ThenInclude(rh => rh.IdHabitacionNavigation)
                .Include(r => r.ReservaComplemento)
                .Include(r => r.IdHuesped)
                .AsNoTracking()
                .ToListAsync();

            return reservas;
        }

        public async Task<Reserva> FindByIdAsync(int id)
        {
            var reserva = await _context.Reserva
                .Include(x => x.ReservaComplemento)
                    .ThenInclude(x => x.IdComplementoNavigation)
                .Include(x => x.IdCruceroNavigation)
                    .ThenInclude(x => x.Itinerario.OrderBy(i => i.Orden))
                        .ThenInclude(x => x.IdPuertoNavigation)
                .Include(x => x.IdCruceroNavigation)
                    .ThenInclude(x => x.FechaCrucero)
                .Include(x => x.ReservaHabitacion)
                    .ThenInclude(x => x.IdHabitacionNavigation)
                        .ThenInclude(x => x.PrecioHabitacion)
                .FirstOrDefaultAsync(x => x.Id == id);
                

            return reserva!;
        }

        public async Task<ICollection<Reserva>> ListAsync()
        {
            var collection = await _context.Set<Reserva>()
                                              .Include(x => x.IdUsuarioNavigation)
                                              .Include(x => x.IdCruceroNavigation)
                                              .AsNoTracking()
                                              .ToListAsync();
            return collection;
        }

        public async Task<ICollection<Reserva>> ListAsyncCliente(int idUsuario)
        {
            var collection = await _context.Set<Reserva>()
                                           .Where(r => r.IdUsuario == idUsuario) 
                                           .Include(r => r.IdUsuarioNavigation)  
                                           .Include(r => r.IdCruceroNavigation)
                                           .AsNoTracking()                     
                                           .ToListAsync();

            return collection;
        }

    }
}
