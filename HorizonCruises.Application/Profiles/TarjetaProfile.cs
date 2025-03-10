using AutoMapper;
using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.Profiles
{
    public class TarjetaProfile : Profile
    {
        public TarjetaProfile()
        {
            CreateMap<TarjetaDTO, Tarjeta>().ReverseMap();
        }
    }
}
