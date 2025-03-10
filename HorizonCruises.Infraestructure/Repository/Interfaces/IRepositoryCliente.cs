using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCliente
    {
        //Task<ICollection<Usuario>> FindByDescriptionAsync(string description);
        Task<ICollection<Usuario>> ListAsync();
        Task<Usuario> FindByIdAsync(int id);
        Task<int> AddAsync(Usuario entity);
        Task DeleteAsync(int id);
        Task UpdateAsync();
    }
}
