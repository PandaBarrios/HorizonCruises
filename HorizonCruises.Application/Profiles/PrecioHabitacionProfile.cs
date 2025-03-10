using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Profiles
{
    public class PrecioHabitacionProfile : Profile
    {
        public PrecioHabitacionProfile() { 
            CreateMap<PrecioHabitacionDTO, PrecioHabitacion>().ReverseMap();
        }
    }
}
