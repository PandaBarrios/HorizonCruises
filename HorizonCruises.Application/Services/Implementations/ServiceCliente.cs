using AutoMapper;
using HorizonCruises.Application.Config;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Application.Utils;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Implementations
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly IRepositoryCliente _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;

        public ServiceCliente(IRepositoryCliente repository, IMapper mapper, IOptions<AppConfig> options)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
        }


        //Gestion de usuarios Inicio

        public async Task<string> AddAsync(ClienteDTO dto)
        {
            //Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(dto.Contrasena!, secret);
            // Establecer password DTO
            dto.Contrasena = passwordEncrypted;
            var objectMapped = _mapper.Map<Usuario>(dto);

            return await _repository.AddAsync(objectMapped);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<ClienteDTO>> FindByDescriptionAsync(string description)
        {
            var list = await _repository.FindByDescriptionAsync(description);
            var collection = _mapper.Map<ICollection<ClienteDTO>>(list);
            return collection;
        }

        public async Task<ClienteDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ClienteDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ClienteDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<ClienteDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task<ClienteDTO> LoginAsync(string email, string password)
        {
            ClienteDTO usuarioDTO = null!;

            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(password, secret);

           


            var @object = await _repository.LoginAsync(email, passwordEncrypted);

            if (@object != null)
            {
                usuarioDTO = _mapper.Map<ClienteDTO>(@object);
            }

            return usuarioDTO;
        }

        public async Task UpdateAsync(int id, ClienteDTO dto)
        {
            var @object = await _repository.FindByIdAsync(id);
            // source, destination
            _mapper.Map(dto, @object!);
            await _repository.UpdateAsync();
        }

        //Gestion de usuarios Fin
    }
}
