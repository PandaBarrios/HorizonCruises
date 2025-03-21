using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServicePrecioHabitacion : IServicePrecioHabitacion
    {

        private readonly IRepositoryPrecioHabitacion _repository;
        private readonly IMapper _mapper;

        public ServicePrecioHabitacion(IRepositoryPrecioHabitacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PrecioHabitacionDTO> CreateAsync(PrecioHabitacionDTO nPrecio)
        {
            if (nPrecio == null)
            {
                throw new ArgumentNullException(nameof(nPrecio), "El objeto Precio Habitacion no puede ser nulo.");
            }
            // Mapear de DTO a entidad
            var precio = _mapper.Map<PrecioHabitacion>(nPrecio);
            // Guardar en la base de datos
            var PrecioCreado = await _repository.CreateAsync(precio);
            // Mapear el resultado nuevamente a DTO
            return _mapper.Map<PrecioHabitacionDTO>(PrecioCreado);
        }

        public async Task<PrecioHabitacionDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            return _mapper.Map<PrecioHabitacionDTO>(@object);
        }

        public async Task<ICollection<PrecioHabitacionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<PrecioHabitacionDTO>>(list);
        }
    }
}
