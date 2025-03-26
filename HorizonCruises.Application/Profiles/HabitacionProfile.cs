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
    public class HabitacionProfile : Profile
    {
        public HabitacionProfile()
        {
            CreateMap<HabitacionDTO, Habitacion>().ReverseMap();
            
            // Mapeo inverso
            CreateMap<Habitacion, HabitacionDTO>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(o => o.Id))
                .ForMember(dest => dest.Nombre, orig => orig.MapFrom(o => o.Nombre))
                .ForMember(dest => dest.Descripcion, orig => orig.MapFrom(o => o.Descripcion))
                .ForMember(dest => dest.CantidadMinimaHuespedes, orig => orig.MapFrom(o => o.CantidadMinimaHuespedes))
                .ForMember(dest => dest.CantidadMaximaHuespedes, orig => orig.MapFrom(o => o.CantidadMaximaHuespedes))
                .ForMember(dest => dest.Tamano, orig => orig.MapFrom(o => o.Tamano))
                .ForMember(dest => dest.Tipo, orig => orig.MapFrom(o => o.Tipo))

                // Mapeo de las colecciones relacionadas
                .ForMember(dest => dest.BarcoHabitaciones, orig => orig.MapFrom(o => o.BarcoHabitaciones))
                .ForMember(dest => dest.PrecioHabitacion, orig => orig.MapFrom(o => o.PrecioHabitacion))
                .ForMember(dest => dest.ReservaHabitacion, orig => orig.MapFrom(o => o.ReservaHabitacion));
        }
    }
}

