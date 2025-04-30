using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceReserva : IServiceReserva
    {
        private readonly IRepositoryReserva _repository;
        private readonly IMapper _mapper;

        public ServiceReserva(IRepositoryReserva repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReservaDTO> CreateAsync(ReservaDTO reserva)
        {
            var entidad = _mapper.Map<Reserva>(reserva);

            // Guardar en la base de datos
            var entidadCreada = await _repository.CreateAsync(entidad);

            // Mapear de nuevo a DTO para incluir el ID generado
            return _mapper.Map<ReservaDTO>(entidadCreada);
        }

        public async Task<ICollection<ReservaDTO>> FiltrarPorRangoFechaAsync(DateOnly fechaInicio, DateOnly fechaFinal)
        {
            var reservas = await _repository.FiltrarPorRangoFechaAsync(fechaInicio, fechaFinal);
            var reservasDTO = _mapper.Map<ICollection<ReservaDTO>>(reservas);
            return reservasDTO;
        }


        public async Task<ReservaDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ReservaDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ReservaDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<ReservaDTO>>(list);
            // Return lista 
            return collection;
        }

        public async Task<ICollection<ReservaDTO>> ListAsyncCliente(int idUsuario)
        {
            // Obtener datos filtrados del repositorio
            var list = await _repository.ListAsyncCliente(idUsuario);

            // Mapear List<Reserva> a ICollection<ReservaDTO>
            var collection = _mapper.Map<ICollection<ReservaDTO>>(list);

            // Devolver la colección mapeada
            return collection;
        }

    }
}
