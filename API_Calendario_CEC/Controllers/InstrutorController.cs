using System.Collections.Generic;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;
using FluentResults;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstrutorController : ControllerBase
    {
        private InstrutorService _instrutorService;

        public InstrutorController(InstrutorService instrutorService)
        {
            _instrutorService = instrutorService;
        }

        [HttpGet]
        public IActionResult listarInstrutores()
        {
            List<ReadInstrutorDto> instrutores = _instrutorService.listarInstrutores();
            if (instrutores == null) return NotFound();
            return Ok(instrutores);
        }

        [HttpGet("{id}")]
        public IActionResult recuperarInstrutorPorId(int id)
        {
            ReadInstrutorDto instrutorDto = _instrutorService.recuperarInstrutorPorId(id);
            if (instrutorDto == null) return NotFound();
            return Ok(instrutorDto);
        }

        [HttpPost]
        public IActionResult criarInstrutor([FromBody] CreateInstrutorDto createInstrutorDto)
        {
            Instrutor instrutor = _instrutorService.criarInstrutor(createInstrutorDto);
            return CreatedAtAction(nameof(recuperarInstrutorPorId), new { Id = instrutor.Id }, instrutor);
        }

        [HttpPut("{id}")]
        public IActionResult atualizarInstrutor(int id, [FromBody] UpdateInstrutorDto updateInstrutorDto)
        {
            Result result = _instrutorService.atualizarInstrutor(id, updateInstrutorDto);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult apagarInstrutor(int id)
        {
            Result result = _instrutorService.apagarInstrutor(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok();
        }

    }
}
