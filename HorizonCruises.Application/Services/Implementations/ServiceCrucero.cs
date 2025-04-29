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
    public class ServiceCrucero : IServiceCrucero
    {
        private readonly IRepositoryCrucero _repository;
        private readonly IMapper _mapper;

        public ServiceCrucero(IRepositoryCrucero repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CruceroDTO> CreateAsync(CruceroDTO cruceroDTO)
        {
            if (cruceroDTO == null)
            {
                throw new ArgumentNullException(nameof(cruceroDTO), "El objeto CruceroDTO no puede ser nulo.");
            }

            // Mapear de DTO a entidad
            var crucero = _mapper.Map<Crucero>(cruceroDTO);

            // Guardar en la base de datos
            var cruceroCreado = await _repository.CreateAsync(crucero);

            // Mapear el resultado nuevamente a DTO
            return _mapper.Map<CruceroDTO>(cruceroCreado);
        }

        public async Task<CruceroDTO> DetalleCrucero(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<CruceroDTO>(@object);
            return objectMapped;
        }

        public async Task<CruceroDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<CruceroDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<CruceroDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Crucero> a ICollection<CruceroDTO> 
            var collection = _mapper.Map<ICollection<CruceroDTO>>(list);
            // Return lista 
            return collection;
        }

        
    }
}
