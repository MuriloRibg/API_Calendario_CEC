using API_Calendario_CEC.Data.Request;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class FullCalendarProfile : Profile
    {
        public FullCalendarProfile()
        {
            CreateMap<Reserva, FullCalendarRequest>();
        }
    }
}
