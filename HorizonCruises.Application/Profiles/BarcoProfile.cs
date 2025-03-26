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
    public class BarcoProfile : Profile
    {
        public BarcoProfile()
        {
            // Mapeo directo entre Barco y BarcoDTO
            CreateMap<Barco, BarcoDTO>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(o => o.Id))
                .ForMember(dest => dest.Nombre, orig => orig.MapFrom(o => o.Nombre))
                .ForMember(dest => dest.Descripcion, orig => orig.MapFrom(o => o.Descripcion))
                .ForMember(dest => dest.CapacidadHuespedes, orig => orig.MapFrom(o => o.CapacidadHuespedes))

                // Mapeo de las colecciones relacionadas (asegurando que las relaciones se mapeen correctamente)
                .ForMember(dest => dest.BarcoHabitaciones, orig => orig.MapFrom(o => o.BarcoHabitaciones))
                .ForMember(dest => dest.Crucero, orig => orig.MapFrom(o => o.Crucero));

            // Mapeo inverso de BarcoDTO a Barco
            CreateMap<BarcoDTO, Barco>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(o => o.Id))
                .ForMember(dest => dest.Nombre, orig => orig.MapFrom(o => o.Nombre))
                .ForMember(dest => dest.Descripcion, orig => orig.MapFrom(o => o.Descripcion))
                .ForMember(dest => dest.CapacidadHuespedes, orig => orig.MapFrom(o => o.CapacidadHuespedes))

                // Mapeo de las colecciones relacionadas (asegurando que las relaciones se mapeen correctamente)
                .ForMember(dest => dest.BarcoHabitaciones, orig => orig.MapFrom(o => o.BarcoHabitaciones))
                .ForMember(dest => dest.Crucero, orig => orig.MapFrom(o => o.Crucero));
        }
    }
}
