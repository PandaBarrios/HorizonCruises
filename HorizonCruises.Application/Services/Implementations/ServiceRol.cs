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
    public class ServiceRol: IServiceRol
    {
        private readonly IRepositoryRol _repository;
        private readonly IMapper _mapper;

        public ServiceRol(IRepositoryRol repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RolDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<RolDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<RolDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Rol> a ICollection<RolDTO> 
            var collection = _mapper.Map<ICollection<RolDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}
