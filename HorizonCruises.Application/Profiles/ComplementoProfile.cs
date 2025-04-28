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
    public class ComplementoProfile : Profile
    {
        public ComplementoProfile()
        {
            CreateMap<ComplementoDTO, Complemento>()
                .ForMember(dest => dest.ReservaComplemento, opt => opt.Ignore())  // Ignorar la propiedad ReservaComplemento
                .ReverseMap();
        }
    }
}
