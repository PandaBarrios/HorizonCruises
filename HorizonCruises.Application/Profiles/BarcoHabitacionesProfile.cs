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
    public class BarcoHabitacionesProfile : Profile
    {
        public BarcoHabitacionesProfile()
        {
            // Mapeo entre DTO y entidad
            CreateMap<BarcoHabitacionesDTO, BarcoHabitaciones>()
                .ForMember(dest => dest.IdBarco, orig => orig.MapFrom(o => o.IdBarco))
                .ForMember(dest => dest.IdHabitacion, orig => orig.MapFrom(o => o.IdHabitacion))
                .ForMember(dest => dest.TotalHabitacionesDisponibles, orig => orig.MapFrom(o => o.TotalHabitacionesDisponibles))

                // Evitar que se mapeen las propiedades de navegación (para evitar error de referencia nula)
                .ForMember(dest => dest.IdBarcoNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IdHabitacionNavigation, opt => opt.Ignore());

            // Mapeo inverso (de entidad a DTO)
            CreateMap<BarcoHabitaciones, BarcoHabitacionesDTO>()
                .ForMember(dest => dest.IdBarco, orig => orig.MapFrom(o => o.IdBarco))
                .ForMember(dest => dest.IdHabitacion, orig => orig.MapFrom(o => o.IdHabitacion))
                .ForMember(dest => dest.TotalHabitacionesDisponibles, orig => orig.MapFrom(o => o.TotalHabitacionesDisponibles));
        }
    }   
}
