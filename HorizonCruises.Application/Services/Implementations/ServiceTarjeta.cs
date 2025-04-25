using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceTarjeta : IServiceTarjeta
    {
        private readonly IRepositoryTarjeta _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceTarjeta> _logger;

        public ServiceTarjeta(IRepositoryTarjeta repository, IMapper mapper, ILogger<ServiceTarjeta> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<TarjetaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<TarjetaDTO>>(list);
        }

        public async Task<TarjetaDTO> FindByIdAsync(int id)
        {
            var tarjeta = await _repository.FindByIdAsync(id);
            return _mapper.Map<TarjetaDTO>(tarjeta);
        }

        public async Task<int> AddAsync(TarjetaDTO dto)
        {
            var tarjeta = _mapper.Map<Tarjeta>(dto);
            return await _repository.AddAsync(tarjeta);
        }

        public async Task UpdateAsync(int id, TarjetaDTO dto)
        {
            var tarjeta = await _repository.FindByIdAsync(id);
            var updated = _mapper.Map(dto, tarjeta!);
            await _repository.UpdateAsync(updated);
        }

        // Método para obtener solo las tarjetas asociadas al IdUsuario
        public async Task<ICollection<TarjetaDTO>> GetTarjetasByUsuarioIdAsync(int userId)
        {
            var tarjetas = await _repository.ListAsync();
            var tarjetasDelUsuario = tarjetas.Where(t => t.IdUsuario == userId).ToList();
            return _mapper.Map<ICollection<TarjetaDTO>>(tarjetasDelUsuario);
        }
    }
}