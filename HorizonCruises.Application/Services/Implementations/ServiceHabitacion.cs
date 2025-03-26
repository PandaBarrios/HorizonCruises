using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceHabitacion : IServiceHabitacion
    {
        private readonly IRepositoryHabitacion _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceHabitacion> _logger;

        public ServiceHabitacion(IRepositoryHabitacion repository, IMapper mapper, ILogger<ServiceHabitacion> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<HabitacionDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<HabitacionDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<HabitacionDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Habitacion> a ICollection<HabitacionDTO> 
            var collection = _mapper.Map<ICollection<HabitacionDTO>>(list);
            // Return lista 
            return collection;
        }

        public async Task<int> AddAsync(HabitacionDTO dto)
        {
            // Map HabitacionDTO to Habitacion
            var objectMapped = _mapper.Map<Habitacion>(dto);

            // Add to repository
            return await _repository.AddAsync(objectMapped);
        }

        public async Task UpdateAsync(int id, HabitacionDTO dto)
        {
            //Obtenga el modelo original a actualizar
            var @object = await _repository.FindByIdAsync(id);
            //       source, destination
            var entity = _mapper.Map(dto, @object!);


            await _repository.UpdateAsync(entity);
        }
    }
}
