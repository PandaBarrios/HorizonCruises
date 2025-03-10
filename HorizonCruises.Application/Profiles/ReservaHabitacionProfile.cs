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
    public class ReservaHabitacionProfile : Profile
    {
        public ReservaHabitacionProfile()
        {
            CreateMap<ReservaHabitacionDTO, ReservaHabitacion>().ReverseMap();
        }
    }
}
