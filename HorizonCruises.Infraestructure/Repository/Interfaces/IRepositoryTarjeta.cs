using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryTarjeta
    {
        Task<ICollection<Tarjeta>> ListAsync();
        Task<Tarjeta> FindByIdAsync(int id);
        Task<int> AddAsync(Tarjeta entity);
        Task UpdateAsync(Tarjeta entity);
        Task<ICollection<Tarjeta>> GetTarjetasByUsuarioIdAsync(int userId);
    }
}
