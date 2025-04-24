using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryHuesped
    {
        Task<ICollection<Huesped>> ListAsync();
        Task<Huesped> AddAsync(Huesped entity);
    }
}
