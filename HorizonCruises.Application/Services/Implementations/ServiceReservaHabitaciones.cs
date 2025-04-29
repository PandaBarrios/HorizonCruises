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
    public class ServiceReservaHabitaciones : IServiceReservaHabitaciones
    {
        private readonly IRepositoryReservaHabitacion _repository;
        private readonly IMapper _mapper;

        public ServiceReservaHabitaciones(IRepositoryReservaHabitacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ReservaHabitacionDTO objeto)
        {
            var entidad = _mapper.Map<ReservaHabitacion>(objeto);
            await _repository.CreateAsync(entidad);
        }

    }
}
