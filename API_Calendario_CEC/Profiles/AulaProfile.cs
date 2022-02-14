using API_Calendario_CEC.Data.Dto.Aulas;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class AulaProfile : Profile
    {
        public AulaProfile()
        {
            CreateMap<CreateAulaDto, Aula>();
            CreateMap<UpdateAulaDto, Aula>();
            CreateMap<Aula, ReadAulaDto>();
        }
    }
}
