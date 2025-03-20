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
    public class ServicePuerto : IServicePuerto
    {
        private readonly IRepositoryPuerto _repository;
        private readonly IMapper _mapper;

        public ServicePuerto(IRepositoryPuerto repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PuertoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<PuertoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<PuertoDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);
            var collection = _mapper.Map<ICollection<PuertoDTO>>(list);
            return collection;
        }

        public async Task<ICollection<PuertoDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Barco> a ICollection<BarcoDTO> 
            var collection = _mapper.Map<ICollection<PuertoDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}
