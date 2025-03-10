using HorizonCruises.Application.DTOs;
using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace HorizonCruises.Application.Profiles
{
    public class DestinoProfile: Profile
    {
        public DestinoProfile()        {
            CreateMap<DestinoDTO, Destino>().ReverseMap();
        }
    }
}
