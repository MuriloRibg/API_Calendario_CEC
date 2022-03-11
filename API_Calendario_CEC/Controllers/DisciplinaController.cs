using API_Calendario_CEC.Data.Dto.Disciplinas;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FluentResults;

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
        public IActionResult ListarDisciplinas([FromQuery] string pesquisa, [FromQuery] int page)
        {
            List<ReadDisciplinaDto> disciplinas = _disciplinaService.ListarDisciplinas(pesquisa, page);
            if(disciplinas == null) return NotFound();

            int qtdTotalDisciplinas;

            if (pesquisa == null || pesquisa == "")
            {
                qtdTotalDisciplinas =  _disciplinaService.QuantidadeTotalDisciplinas();
            }
            else
            {
                qtdTotalDisciplinas = _disciplinaService.QuantidadeTotalPesquisa(pesquisa);
            }
            
            return Ok(new {
                disciplinas,
                qtdTotalDisciplinas
            });
        }

        [HttpGet("pilar")]
        public IActionResult ListarDisciplinasPorPilar([FromQuery] string pilar)
        {
            List<ReadDisciplinaDto> disciplinaDtos = _disciplinaService.ListarDisciplinasPorPilar(pilar);
            if (disciplinaDtos == null) return NotFound();
            return Ok(disciplinaDtos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarDisciplinaPorId(int id)
        {
            ReadDisciplinaDto disciplinaDto = _disciplinaService.RecuperarDisciplinaPorId(id);
            if(disciplinaDto == null) return NotFound();
            return Ok(disciplinaDto);
        }

        [HttpGet("validar/{nomeDisciplina}")]
        public IActionResult VerificarNomeDisciplina(string nomeDisciplina)
        {
            Result resultado = _disciplinaService.VerificarNomeDisciplina(nomeDisciplina);
            if (resultado.IsFailed) return Ok(new { resultado.Reasons, status = true });
            return Ok(new { resultado.Reasons, status = false }); 
        }

        [HttpPost]
        public IActionResult CriaDisciplina([FromBody] CreateDisciplinaDto disciplinaDto)
        {
            Result<Disciplina> resultado = _disciplinaService.CriaDisciplina(disciplinaDto);
            if (resultado.IsFailed) return NotFound(resultado.Reasons);

            return CreatedAtAction(
                nameof(RecuperarDisciplinaPorId), new { Id = resultado.Value.Id }, resultado.Value);
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
