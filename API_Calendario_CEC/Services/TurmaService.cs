using System;
using System.Collections.Generic;
using System.Linq;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using PagedList;

namespace API_Calendario_CEC.Services
{
    public class TurmaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public TurmaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int QuantidadeTotalTurmas() {
            int total = _context.Turmas.Where(turma => turma.DeleteAt == null).Count();
            return total;
        }

        public int QuantidadeTotalPesquisa(string pesquisa) {
            return _context.Turmas.Where(turma =>
                turma.DeleteAt == null &&
                    (
                        turma.Nome.ToLower().Contains(pesquisa) ||
                        turma.Pilar.NomePilar.ToLower().Contains(pesquisa) ||
                        turma.Pilar.Categoria.ToLower().Contains(pesquisa)
                    )
            )
            .Count();
        }

        //GET
        public List<ReadTurmasDto> ListarTurmas(string? pesquisa, int? page)
        {
            List<Turma> turmas;
            if (pesquisa == null || pesquisa == "")
            {
                turmas = _context.Turmas
                    .Where(turma => turma.DeleteAt == null)
                    .ToList();
            }
            else
            {
                pesquisa = pesquisa.ToLower();
                
                turmas = _context.Turmas
                    .Where(turma =>
                        turma.DeleteAt == null &&
                        (
                            turma.Nome.ToLower().Contains(pesquisa) ||
                            turma.Pilar.NomePilar.ToLower().Contains(pesquisa) ||
                            turma.Pilar.Categoria.ToLower().Contains(pesquisa)
                        )
                    )
                    .ToList();
            }

            if (turmas != null && page != null && page != 0)
            {
                int pageSize = 6;
                int currentPage = (page ?? 1);
                return _mapper.Map<List<ReadTurmasDto>>(turmas.ToPagedList(currentPage, pageSize));
            }

            return _mapper.Map<List<ReadTurmasDto>>(turmas);
        }

        public Result validarNomeTurma(string nomeTurma) {
            Turma turma = _context.Turmas.FirstOrDefault(turma => turma.DeleteAt == null 
                && turma.Nome.ToUpper() == nomeTurma.ToUpper());
            if (turma == null) return Result.Ok();
            return Result.Fail("Nome de turma em uso!");
        }   

        public List<ReadTurmasDto> ListarTurmaPorPilar(string? pilar) {
            List<Turma> turmas = _context.Turmas
                .Where(turma => turma.DeleteAt == null &&
                    turma.Pilar.NomePilar.ToUpper() == pilar.ToUpper())
                .ToList();
            return _mapper.Map<List<ReadTurmasDto>>(turmas);
        }

        //GET ID
        public ReadTurmasDto RecuperarTurmaPorId(int id)
        {
            Turma turma = _context.Turmas
                .FirstOrDefault(turma => turma.Id == id && turma.DeleteAt == null);

            if (turma == null) return null;
            return _mapper.Map<ReadTurmasDto>(turma); 
        }


        //POST
        public Result<ReadTurmasDto> CriarTurma(CreateTurmasDto createTurmaDto)
        {
            Turma turmaCadastrado = verificaNomeTurma(_mapper.Map<Turma>(createTurmaDto));

            if(turmaCadastrado == null)
            {
                Turma turma = _mapper.Map<Turma>(createTurmaDto);                
                _context.Add(turma);    
                _context.SaveChanges();
                ReadTurmasDto readTurmaDto = _mapper.Map<ReadTurmasDto>(turma);
                return Result.Ok(readTurmaDto).ToResult(s => s);
            }
            return Result.Fail($"Turma {turmaCadastrado.Nome} já cadastrada");
        }

        //PUT
        public Result AtualizarTurma(int id, UpdateTurmasDto updateTurmaDto)
        {
            Turma turmaCadastrado = verificaNomeTurma(_mapper.Map<Turma>(updateTurmaDto));
            if(turmaCadastrado != null) return Result.Fail($"Turma {turmaCadastrado.Nome} já cadastrada");

            Turma turma = _context.Turmas
                .FirstOrDefault(turma => turma.Id == id);

            if (turma == null) return Result.Fail("Turma não encontrada");
            _mapper.Map(updateTurmaDto, turma);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Turma atualizada com sucesso!");
        }

        //DELETE
        public Result ApagarTurma(int id)
        {
            Turma turma = _context.Turmas
                .FirstOrDefault(turma => turma.Id == id);

            if (turma == null) return Result.Fail("Turma não encontrada!");
            turma.DeleteAt = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Turma apagada com sucesso!");
        }

        //PUT restaurar
        public Result RestaurarTurmaPorID(int id)
        {
            Turma turma = _context.Turmas
                .FirstOrDefault(turma => turma.Id == id);

            if (turma == null) return Result.Fail("Turma não encontrado!");
            turma.DeleteAt = null;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Turma restaurado com sucesso!");
        }

        private Turma verificaNomeTurma(Turma turmaCadastro)
        {
            Turma turmaCadastrado = _context
                .Turmas
                .FirstOrDefault(turma => turma.Nome.ToUpper() == turmaCadastro.Nome.ToUpper());
            return turmaCadastrado;
        }
    }
}
