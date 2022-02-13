﻿using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Disciplinas;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
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

        //GET
        public List<ReadDisciplinaDto> listarDisciplinas()
        {
            List<Disciplina> disciplinas = _context.Disciplinas
                .Where(instrutor => instrutor.DeleteAt == null)
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
        public Disciplina CriaDisciplina(CreateDisciplinaDto disciplinaDto)
        {
            Disciplina disciplinaCadastrado = _mapper.Map<Disciplina>(disciplinaDto);
            _context.Add(disciplinaCadastrado);
            _context.SaveChanges();
            return disciplinaCadastrado;
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
