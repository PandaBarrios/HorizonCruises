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
    public class ServiceReservaComplementos : IServiceReservaComplementos
    {
        private readonly IRepositoryReservaComplementos _repository;
        private readonly IMapper _mapper;

        public ServiceReservaComplementos(IRepositoryReservaComplementos repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(int idReserva, int idComplemento, int cantidad)
        {
            var entidad = new ReservaComplemento
            {
                IdReserva = idReserva,
                IdComplemento = idComplemento,
                Cantidad = cantidad
            };

            await _repository.CreateAsync(entidad);
        }

    }
}
