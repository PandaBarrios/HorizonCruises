using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceItinerario : IServiseItinerario
    {
        private readonly IRepositoryItinerario _repository;
        private readonly IMapper _mapper;

        public ServiceItinerario(IRepositoryItinerario repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ItinerarioDTO> CreateAsync(ItinerarioDTO nDTO)
        {
            if (nDTO == null)
            {
                throw new ArgumentNullException(nameof(nDTO), "El objeto CruceroDTO no puede ser nulo.");
            }
            // Mapear de DTO a entidad
            var itinerario = _mapper.Map<Itinerario>(nDTO);
            // Guardar en la base de datos
            var ItinerarioCreado = await _repository.CreateAsync(itinerario);
            // Mapear el resultado nuevamente a DTO
            return _mapper.Map<ItinerarioDTO>(ItinerarioCreado);
        }

        public async Task<ItinerarioDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            return _mapper.Map<ItinerarioDTO>(@object);
        }

        public async Task<ICollection<ItinerarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<ItinerarioDTO>>(list);
        }
    }
}

