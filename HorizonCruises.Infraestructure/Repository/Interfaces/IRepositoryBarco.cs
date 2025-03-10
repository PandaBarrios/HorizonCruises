using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorizonCruises.Infraestructure.Models;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryBarco
    {
        Task<ICollection<Barco>> ListAsync();
        Task<Barco> FindByIdAsync(int id);
    }
}
