using API_Calendario_CEC.Data.Dto.Disciplinas;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private DisciplinaService _disciplinaService;

        public DisciplinaController(DisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        [HttpGet]
        public IActionResult ListarDisciplinas()
        {
            List<ReadDisciplinaDto> disciplinas = _disciplinaService.listarDisciplinas();
            if(disciplinas == null) return NotFound();
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarDisciplinaPorId(int id)
        {
            ReadDisciplinaDto disciplinaDto = _disciplinaService.RecuperarDisciplinaPorId(id);
            if(disciplinaDto == null) return NotFound();
            return Ok(disciplinaDto);
        }

        [HttpPost]
        public IActionResult CriaDisciplina([FromBody] CreateDisciplinaDto disciplinaDto)
        {
            Console.WriteLine(disciplinaDto);
            Disciplina disciplina = _disciplinaService.CriaDisciplina(disciplinaDto);
            return CreatedAtAction(nameof(RecuperarDisciplinaPorId), new { Id = disciplina.Id }, disciplina);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaDisciplina([FromBody] UpdateDisiciplinaDto updateDisiciplina, int id)
        {
            Result result = _disciplinaService.AtualizaDisciplina(id, updateDisiciplina);
            if(result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaDisciplina(int id)
        {
            Result result = _disciplinaService.DeletaDiscipliana(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }
    }
}
