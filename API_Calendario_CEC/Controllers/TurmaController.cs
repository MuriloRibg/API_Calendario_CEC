using System.Collections.Generic;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using System;

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

        [HttpGet("pilar")]
        public IActionResult ListarTurmasPorPilar([FromQuery] string pilar) {
            List<ReadTurmasDto> turmasPorPilar = _turmaService.ListarTurmaPorPilar(pilar);
            if (turmasPorPilar == null) return NotFound();
            return Ok(turmasPorPilar);  
        }

        [HttpGet]
        public IActionResult ListarTurmas([FromQuery] string pesquisa, [FromQuery] int page) 
        {
            List<ReadTurmasDto> turmas = _turmaService.ListarTurmas(pesquisa, page);
            if (turmas == null) return NotFound();

            int qtdTotalTurmas = 0;

            if (pesquisa == null || pesquisa == "") {
                qtdTotalTurmas = _turmaService.QuantidadeTotalTurmas();
            }
            else {
                qtdTotalTurmas = _turmaService.QuantidadeTotalPesquisa(pesquisa);
            }

            return Ok(new {
                turmas,
                qtdTotalTurmas
            });
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
            Result<ReadTurmasDto> resultadoTurma = _turmaService.CriarTurma(turmaDto);
            if(resultadoTurma.IsFailed) return BadRequest(resultadoTurma.Reasons);
            ReadTurmasDto turmaCadastrada = resultadoTurma.Value;
            return CreatedAtAction(nameof(RecuperarTurmaPorId), new { Id = turmaCadastrada.Id }, turmaCadastrada);
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
