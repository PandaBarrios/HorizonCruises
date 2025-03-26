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

        public async Task<int> AddAsync(Habitacion entity)
        {
            try
            {
                _context.Habitacion.Add(entity);
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message;
                Console.WriteLine($"Error interno: {innerMessage}");

                // Opcional: También puedes registrar el error en logs
                // _logger.LogError($"Error al agregar habitación: {innerMessage}");

                // Retorna -1 o lanza una excepción personalizada
                return -1;
            }
            catch (Exception ex)  // Captura otros errores inesperados
            {
                Console.WriteLine($"Error desconocido: {ex.Message}");
                return -1;
            }
        }

        public async Task UpdateAsync(Habitacion entity)
        {
            var habitacionExistente = await _context.Habitacion.FindAsync(entity.Id);

            if (habitacionExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró la habitación con ID {entity.Id}");
            }

            // Actualizar los valores necesarios
            habitacionExistente.CantidadMaximaHuespedes = entity.CantidadMaximaHuespedes;
            habitacionExistente.CantidadMinimaHuespedes = entity.CantidadMinimaHuespedes;
            habitacionExistente.Descripcion = entity.Descripcion;
            habitacionExistente.Nombre = entity.Nombre;
            habitacionExistente.Tamano = entity.Tamano;
            habitacionExistente.Tipo = entity.Tipo;

            await _context.SaveChangesAsync();
        }
    }
}
