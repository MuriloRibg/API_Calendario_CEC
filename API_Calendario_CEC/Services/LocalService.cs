using System;
using System.Collections.Generic;
using System.Linq;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Locais;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using PagedList;

namespace API_Calendario_CEC.Services {
    public class LocalService {
        
        private AppDbContext _context;
        private IMapper _mapper;

        public LocalService(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        // Retorna quantidade total de locais
        public int QuantidadeTotalLocais()
        {
            int total = _context.Locais.Where(local => local.DeleteAt == null).Count();
            return total;
        }

        public int QuantidadeTotalPesquisa(string pesquisa)
        {
            return _context.Locais
                    .Where(local => local.DeleteAt == null &&
                        (
                            local.Capacidade.ToString().Contains(pesquisa) ||
                            local.Nome.ToLower().Contains(pesquisa)
                        )
                    )
                    .Count();
        }


        //GET
        public List<ReadLocaisDto> ListarLocais(string? pesquisa, int? page) {

            List<Local> locais;
            if (pesquisa == null || pesquisa == "")
            {
                locais = _context.Locais
                    .OrderBy(local => local.Nome)
                    .Where(local => local.DeleteAt == null)
                    .ToList();
            }
            else
            {
                locais = _context.Locais
                    .OrderBy(local => local.Nome)
                    .Where(local => local.DeleteAt == null && 
                        (
                            local.Capacidade.ToString().Contains(pesquisa) ||
                            local.Nome.ToLower().Contains(pesquisa)
                        )
                    )
                    .ToList();
            }
            if (locais != null && page != null && page != 0)
            {
                int pageSize = 6;
                int currentPage = (page ?? 1);
                return _mapper.Map<List<ReadLocaisDto>>(locais.ToPagedList(currentPage, pageSize));
            }
            return _mapper.Map<List<ReadLocaisDto>>(locais);
        }

        public List<ReadLocaisDto> RecuperarLocalPorQuantidade(int qtd_alunos)
        {
            List<Local> local = _context.Locais
                .OrderBy(local => local.Nome)
                .Where(local => local.DeleteAt == null &&
                    local.Capacidade >= qtd_alunos).ToList();

            if (local == null) return null;
            return _mapper.Map<List<ReadLocaisDto>>(local);
        }

        //GET ID
        public ReadLocaisDto RecuperarLocalPorId(int id) {
            Local local = _context.Locais
                .FirstOrDefault(local => local.Id == id && local.DeleteAt == null);
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
            local.DeleteAt = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Local apagado com sucesso!");
        }

    }
}