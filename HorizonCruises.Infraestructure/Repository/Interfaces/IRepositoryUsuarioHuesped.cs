using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUsuarioHuesped
    {
        Task<ICollection<UsuarioHuesped>> GetHuespedByUsuarioAsync(int idUsuario);
        Task<UsuarioHuesped> AddAsync(UsuarioHuesped entity);
    }
}
