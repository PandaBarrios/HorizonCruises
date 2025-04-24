using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Services.Interfaces
{
    public interface IServiceUsuarioHuesped
    {
      Task<ICollection<UsuarioHuespedDTO>> GetHuespedByUsuarioAsync(int idUsuario);
      Task<UsuarioHuespedDTO> AddAsync(int idUsuario, int idHuesped);
    }
}
