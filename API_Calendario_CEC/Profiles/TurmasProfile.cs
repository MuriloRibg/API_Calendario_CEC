using System;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles
{
    public class TurmasProfile : Profile
    {
        public TurmasProfile()
        {
            CreateMap<Turma, ReadTurmasDto>();
        }
    }
}
