using API_Calendario_CEC.Data.Dto.Pilares;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class ProfilePilar : Profile
    {
        public ProfilePilar()
        {
            CreateMap<CreatePilarDto, Pilar>();
            CreateMap<UpdatePilarDto, Pilar>();
            CreateMap<Pilar, ReadPilarDto>();
        }
    }
}
