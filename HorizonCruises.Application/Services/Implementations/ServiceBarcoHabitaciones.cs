using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceBarcoHabitaciones : IServiceBarcoHabitaciones
    {
        private readonly IRepositoryBarcoHabitaciones _repository;
        private readonly IMapper _mapper;

        public ServiceBarcoHabitaciones(IRepositoryBarcoHabitaciones repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarcoHabitacionesDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<BarcoHabitacionesDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<BarcoHabitacionesDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<BarcoHabitaciones> a ICollection<BarcoHabitacionesDTO> 
            var collection = _mapper.Map<ICollection<BarcoHabitacionesDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}
