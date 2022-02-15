using API_Calendario_CEC.Data.Dto.Eventos;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<CreateEventoDto, Evento>();
            CreateMap<UpdateEventoDto, Evento>();
            CreateMap<Evento, ReadEventoDto>();
        }
    }
}
