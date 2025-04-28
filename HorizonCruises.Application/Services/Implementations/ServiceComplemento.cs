using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Implementations;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ServiceComplemento> _logger;

        public ServiceComplemento(IRepositoryComplemento repository, IMapper mapper, ILogger<ServiceComplemento> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<ComplementoDTO>> ListAsync()
        {
            var lista = await _repository.ListAsync();
            var coleccion = _mapper.Map<ICollection<ComplementoDTO>>(lista);
            return coleccion;
        }

        public async Task<ComplementoDTO> FindByIdAsync(int id)
        {
            var @objeto = await _repository.FindByIdAsync(id);
            var @objetoMapeado = _mapper.Map<ComplementoDTO>(@objeto);
            return objetoMapeado;
        }

        public async Task<int> AddAsync(ComplementoDTO dto)
        {
            var objetoMapeado = _mapper.Map<Complemento>(dto);
            return await _repository.AddAsync(objetoMapeado);
        }

        public async Task UpdateAsync(int id, ComplementoDTO dto)
        {
            var @objeto = await _repository.FindByIdAsync(id);
            var entity = _mapper.Map(dto, @objeto);
            await _repository.UpdateAsync(entity);
        }
    }
}
