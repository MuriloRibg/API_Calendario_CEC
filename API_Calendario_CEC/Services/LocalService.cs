using System.Collections.Generic;
using System.Linq;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Locais;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;

namespace API_Calendario_CEC.Services {
    public class LocalService {
        
        private AppDbContext _context;
        private IMapper _mapper;

        public LocalService(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        //GET
        public List<ReadLocaisDto> ListarLocais() {
            List<Local> locais =_context.Locais.ToList();
            if (locais == null) return null;
            return _mapper.Map<List<ReadLocaisDto>>(locais);
        }
        
        //GET ID
        public ReadLocaisDto RecuperarLocalPorId(int id) {
            Local local = _context.Locais.FirstOrDefault(local => local.Id == id);
            if (local == null) return null;
            return _mapper.Map<ReadLocaisDto>(local);
        }

        //POST
        public Local CriarLocal(CreateLocaisDto localDto) {
            Local local = _mapper.Map<Local>(localDto);
            _context.Locais.Add(local);
            _context.SaveChanges();
            return local;
        }

        //PUT 
        public Result AtualizarLocal(int id, UpdateLocaisDto updateLocalDto) {
            Local local = _context.Locais.FirstOrDefault(local => local.Id == id);
            if (local == null) return Result.Fail("Local não encontrado");
            _mapper.Map(updateLocalDto, local);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Local atualizado com sucesso!");
        }

        //DELETE
        public Result ApagarLocal(int id) {
            Local local = _context.Locais.FirstOrDefault(local => local.Id == id);
            if (local == null) return Result.Fail("Local não encontrado");
            _context.Remove(local);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Local apagado com sucesso!");
        }

    }
}