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
    public class ServiceUsuarioHuesped : IServiceUsuarioHuesped
    {
        private readonly IRepositoryUsuarioHuesped _repository;
        private readonly IMapper _mapper;

        public ServiceUsuarioHuesped(IRepositoryUsuarioHuesped repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioHuespedDTO> AddAsync(int idUsuario, int idHuesped)
        {
            var entity = new UsuarioHuespedDTO
            {
                IdHuesped = idHuesped,
                IdUsuario = idUsuario
            };

            var objectMapped = _mapper.Map<UsuarioHuesped>(entity);

            var created = await _repository.AddAsync(objectMapped);

            return _mapper.Map<UsuarioHuespedDTO>(created);
        }



        public async Task<ICollection<UsuarioHuespedDTO>> GetHuespedByUsuarioAsync(int idUsuario)
        {
            var list = await _repository.GetHuespedByUsuarioAsync(idUsuario);
            var collection = _mapper.Map<ICollection<UsuarioHuespedDTO>>(list);
            return collection;
        }
    }
}
