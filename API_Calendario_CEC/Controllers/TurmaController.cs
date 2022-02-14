using System.Collections.Generic;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using API_Calendario_CEC.Profiles;
using Microsoft.AspNetCore.Mvc;
using FluentResults;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmaController : ControllerBase
    {
        private TurmaService _turmaService;

        public TurmaController(TurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet]
        public IActionResult ListarTurmas()
        {
            List<ReadTurmasDto> turmas = _turmaService.ListarTurmas();
            if(turmas == null) return NotFound();
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarTurmaPorId(int id)
        {
            ReadTurmasDto turmaDto = _turmaService.RecuperarTurmaPorId(id);
            if(turmaDto == null) return NotFound();
            return Ok(turmaDto);
        }

        [HttpPost]
        public IActionResult CriaTurma([FromBody] CreateTurmasDto turmaDto)
        {
            Turma turma = _turmaService.CriarTurma(turmaDto);
            return CreatedAtAction(nameof(RecuperarTurmaPorId), new { Id = turma.Id }, turma);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaTurma([FromBody] UpdateTurmasDto updateTurma, int id)
        {
            Result result = _turmaService.AtualizarTurma(id, updateTurma);
            if(result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaTurma(int id)
        {
            Result result = _turmaService.ApagarTurma(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }
    }
}
