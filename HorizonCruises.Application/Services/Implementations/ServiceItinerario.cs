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
    public class ServiceItinerario : IServiseItinerario
    {
        private readonly IRepositoryBarco _repository;
        private readonly IMapper _mapper;

        public ServiceItinerario(IRepositoryBarco repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ItinerarioDTO> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ItinerarioDTO>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
