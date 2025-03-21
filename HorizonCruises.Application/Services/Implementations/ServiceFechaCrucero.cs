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
    public class ServiceFechaCrucero : IServiceFechaCrucero
    {
        private readonly IRepositoryFechaCrucero _repository;
        private readonly IMapper _mapper;

        public ServiceFechaCrucero(IRepositoryFechaCrucero repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FechaCruceroDTO> CreateAsync(FechaCruceroDTO FechaDTO)
        {
            if (FechaDTO == null)
            {
                throw new ArgumentNullException(nameof(FechaDTO), "El objeto CruceroDTO no puede ser nulo.");
            }

            // Mapear de DTO a entidad
            var fecha = _mapper.Map<FechaCrucero>(FechaDTO);

            // Guardar en la base de datos
            var fechaCreada = await _repository.CreateAsync(fecha);

            // Mapear el resultado nuevamente a DTO
            return _mapper.Map<FechaCruceroDTO>(fechaCreada);
        }

        public async Task<FechaCruceroDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<FechaCruceroDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<FechaCruceroDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Barco> a ICollection<BarcoDTO> 
            var collection = _mapper.Map<ICollection<FechaCruceroDTO>>(list);
            return collection;
        }
    }
}
