using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Application.Utils;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceHuesped : IServiceHuesped
    {
        private readonly IRepositoryHuesped _repository;
        private readonly IMapper _mapper;

        public ServiceHuesped(IRepositoryHuesped repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HuespedDTO> AddAsync(HuespedDTO entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entidad nula");
            }

            var objeto = _mapper.Map<Huesped>(entity);

            var objetoCreado = await _repository.AddAsync(objeto);

            return _mapper.Map<HuespedDTO>(objetoCreado);
        }

        public async Task<ICollection<HuespedDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<HuespedDTO>>(list);
            return collection;
        }
    }
}
