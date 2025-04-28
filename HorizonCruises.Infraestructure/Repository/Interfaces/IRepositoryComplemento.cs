using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryComplemento
    {
        Task<ICollection<Complemento>> ListAsync();
        Task<Complemento> FindByIdAsync(int id);
        Task<int> AddAsync(Complemento entity);
        Task UpdateAsync(Complemento entity);
    }
}