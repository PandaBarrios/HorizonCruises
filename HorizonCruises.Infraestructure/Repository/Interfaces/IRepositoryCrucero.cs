﻿using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCrucero
    {
        Task<ICollection<Crucero>> ListAsync();
        Task<Crucero> FindByIdAsync(int id);
        Task<Crucero> CreateAsync(Crucero crucero);
        Task<Crucero> DetalleCrucero(int id);
    }
}
