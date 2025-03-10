using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceReserva : IServiceReserva
    {
        private readonly IRepositoryReserva _repository;
        private readonly IMapper _mapper;

        public ServiceReserva(IRepositoryReserva repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReservaDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ReservaDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ReservaDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<ReservaDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}
