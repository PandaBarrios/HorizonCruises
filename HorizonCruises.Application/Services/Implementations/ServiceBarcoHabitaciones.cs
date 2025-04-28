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

        //Obtiene las habitaciones de un barco en especifico
        public async Task<ICollection<BarcoHabitacionesDTO>> GetHabitacionesByBarcoAsync(int idBarco)
        {
            var list = await _repository.GetHabitacionesByBarcoAsync(idBarco);
            var collection = _mapper.Map<ICollection<BarcoHabitacionesDTO>>(list);
            return collection;
        }

        public async Task<BarcoHabitacionesDTO> GetHabitacionPorId(int id)
        {
            var @object = await _repository.GetHabitacionPorId(id);
            var objectMapped = _mapper.Map<BarcoHabitacionesDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<BarcoHabitacionesDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<BarcoHabitacionesDTO>>(list);
            return collection;
        }

        public async Task UpdateAsync(int idBarco, int idHabitacion, int disponibles)
        {
            await _repository.UpdateAsync(idBarco, idHabitacion, disponibles);
        }
    }
}
