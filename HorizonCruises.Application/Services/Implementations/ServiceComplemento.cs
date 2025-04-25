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
    public class ServiceComplemento: IServiceComplemento
    {
        private readonly IRepositoryComplemento _repository;
        private readonly IMapper _mapper;

        public ServiceComplemento(IRepositoryComplemento repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ComplementoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ComplementoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ComplementoDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<ComplementoDTO>>(list);
            return collection;
        }
    }
}
