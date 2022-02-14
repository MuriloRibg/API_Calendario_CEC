using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<CreateReservaDto, Reserva>();
            CreateMap<UpdateReservaDto, Reserva>();
            CreateMap<Reserva, ReadReservaDto>();
        }
    }
}
