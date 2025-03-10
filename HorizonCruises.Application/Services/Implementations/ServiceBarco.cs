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
    public class ServiceBarco : IServiceBarco
    {
        private readonly IRepositoryBarco _repository;
        private readonly IMapper _mapper;

        public ServiceBarco(IRepositoryBarco repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarcoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<BarcoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<BarcoDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Barco> a ICollection<BarcoDTO> 
            var collection = _mapper.Map<ICollection<BarcoDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}
