using API_Calendario_CEC.Data.Dto.Disciplinas;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class DisciplinaProfile : Profile
    {
        public DisciplinaProfile()
        {
            CreateMap<CreateDisciplinaDto, Disciplina>();
            CreateMap<UpdateDisiciplinaDto, Disciplina>();
            CreateMap<Disciplina, ReadDisciplinaDto>();
        }
    }
}
