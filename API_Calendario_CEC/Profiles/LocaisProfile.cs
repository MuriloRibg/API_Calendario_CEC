using System;
using API_Calendario_CEC.Data.Dto.Locais;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Profiles {
    public class LocaisProfile : Profile {
        
        public LocaisProfile() {
            CreateMap<CreateLocaisDto, Local>();
            CreateMap<UpdateLocaisDto, Local>();
            CreateMap<Local, ReadLocaisDto>();
        }

    }
}