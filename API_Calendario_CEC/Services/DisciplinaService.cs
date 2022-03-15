using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Disciplinas;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class DisciplinaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public DisciplinaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int QuantidadeTotalDisciplinas()
        {
            int quantidadeTotal = _context.Disciplinas
                .Where(disciplina => disciplina.DeleteAt == null).Count();
            return quantidadeTotal;
        }

        public int QuantidadeTotalPesquisa(string pesquisa)
        {
            return _context.Disciplinas
               .Where(disciplina =>
                   disciplina.Nome.Contains(pesquisa) ||
                   disciplina.Pilar.Contains(pesquisa)
                   ).Count();
        }

        //GET
        public List<ReadDisciplinaDto> ListarDisciplinas(string? pesquisa, int? page)
        {
            List<Disciplina> disciplinas;
            if (pesquisa == null || pesquisa == "")
            {
                disciplinas = _context.Disciplinas
                    .Where(disciplina => disciplina.DeleteAt == null)
                    .ToList();
                
            }
            else {
                pesquisa = pesquisa.ToLower();

                disciplinas = _context.Disciplinas
                    .Where(disciplina =>
                        disciplina.Nome.Contains(pesquisa) ||
                        disciplina.Pilar.Contains(pesquisa)
                        ).ToList();
            }
            if (disciplinas != null && page != null && page != 0)
            {
                int pageSize = 6;
                int currentPage = (page ?? 1);
                return _mapper.Map<List<ReadDisciplinaDto>>(disciplinas.ToPagedList(currentPage, pageSize));
            }

            return _mapper.Map<List<ReadDisciplinaDto>>(disciplinas);
        }

        public Result VerificarNomeDisciplina(string nomeDisciplina)
        {
            Disciplina disciplina = _context.Disciplinas
                .FirstOrDefault(disciplina => disciplina.DeleteAt == null &&
                    disciplina.Nome.ToUpper() == nomeDisciplina.ToUpper()
                );
            if (disciplina == null) return Result.Ok();
            return Result.Fail("Nome em uso!");
        }

        public List<ReadDisciplinaDto> ListarDisciplinasPorPilar(string pilar)
        {
            List<Disciplina> disciplinas = _context.Disciplinas
                .Where(disciplina =>
                    disciplina.DeleteAt == null &&
                    disciplina.Pilar.ToUpper() == pilar.ToUpper()
                )
                .ToList();
            if (disciplinas == null) return null;
            return _mapper.Map<List<ReadDisciplinaDto>>(disciplinas);
        }


        //GET ID
        public ReadDisciplinaDto RecuperarDisciplinaPorId(int id)
        {
            Disciplina disciplina = _context.Disciplinas
                .FirstOrDefault(disciplina => disciplina.Id == id && disciplina.DeleteAt == null);
            if (disciplina == null) return null;
            return _mapper.Map<ReadDisciplinaDto>(disciplina);
        }

        //POST
        public Result<Disciplina> CriaDisciplina(CreateDisciplinaDto disciplinaDto)
        {
            Disciplina disciplina = _context.Disciplinas
                .FirstOrDefault(disciplina => disciplina.DeleteAt == null &&
                disciplina.Nome.ToUpper() == disciplinaDto.Nome.ToUpper());
            if(disciplina == null)
            {
                Disciplina disciplinaCadastrado = _mapper.Map<Disciplina>(disciplinaDto);
                _context.Add(disciplinaCadastrado);
                _context.SaveChanges();
                return Result.Ok(disciplinaCadastrado).ToResult(d => d);
            }
            return Result.Fail("Nome da disciplina está em uso!");            
        }

        //PUT
        public Result AtualizaDisciplina(int id, UpdateDisiciplinaDto updatedisciplinaDto)
        {
            Disciplina disciplina = _context.Disciplinas.FirstOrDefault(disciplina => disciplina.Id == id);
            if (disciplina == null) return Result.Fail("Disciplina não encontrada!");
            _mapper.Map(updatedisciplinaDto, disciplina);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Disciplina atualizada com sucesso!");
        } 

        //DELETE
        public Result DeletaDiscipliana(int id)
        {
            Disciplina disciplina = _context.Disciplinas.FirstOrDefault(disciplina => disciplina.Id == id);
            if (disciplina == null) return Result.Fail("Disciplina não encontrada!");
            disciplina.DeleteAt = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Disciplina apagada com sucesso!");
        }
    }
}
