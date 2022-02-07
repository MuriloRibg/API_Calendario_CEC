using System;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class InstrutoresProfile : Profile
    {
        public InstrutoresProfile()
        {
            CreateMap<CreateInstrutorDto, Instrutor>();
            CreateMap<UpdateInstrutorDto, Instrutor>();
            CreateMap<Instrutor, ReadInstrutorDto>();
        }
    }
}
