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
    public class ServiceReservaHuespedes : IServiceReservaHuespedes
    {
        private readonly IRepositoryReservaHuespedes _repository;
        private readonly IMapper _mapper;

        public ServiceReservaHuespedes(IRepositoryReservaHuespedes repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ReservaHuespedDTO objeto)
        {
            var entidad = _mapper.Map<ReservaHuesped>(objeto);
            await _repository.CreateAsync(entidad);
        }

    }
}
