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
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<BarcoDTO>>(list);
            return collection;
        }

        public async Task<bool> AddAsync(BarcoDTO dto)
        {
            if (dto == null) return false;

            var barco = new Barco
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CapacidadHuespedes = (int)dto.CapacidadHuespedes,
                BarcoHabitaciones = dto.BarcoHabitaciones.Select(h => new BarcoHabitaciones
                {
                    IdHabitacion = h.IdHabitacion,
                    TotalHabitacionesDisponibles = (int)h.TotalHabitacionesDisponibles
                }).ToList()
            };

            return await _repository.AddAsync(barco);
        }

        public async Task<bool> UpdateAsync(BarcoDTO dto)
        {
            var barco = await _repository.FindByIdAsync(dto.Id);
            if (barco == null) return false;

            barco.Nombre = dto.Nombre;
            barco.Descripcion = dto.Descripcion;
            barco.CapacidadHuespedes = (int)dto.CapacidadHuespedes;

            // Eliminar las habitaciones antiguas en la base de datos
            barco.BarcoHabitaciones.Clear(); // Esto solo limpia en memoria, falta persistir el cambio

            // Alternativa para asegurarse de que se eliminen en la base de datos si usas Entity Framework
            _repository.RemoveHabitacionesByBarcoId(barco.Id); // Método que debes crear en tu repositorio

            // Agregar nuevas habitaciones
            barco.BarcoHabitaciones = dto.BarcoHabitaciones.Select(h => new BarcoHabitaciones
            {
                IdHabitacion = h.IdHabitacion,
                TotalHabitacionesDisponibles = h.TotalHabitacionesDisponibles
            }).ToList();

            return await _repository.UpdateAsync(barco);
        }
    }
}
