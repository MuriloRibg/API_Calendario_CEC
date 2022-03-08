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
    public class InstrutorController : ControllerBase
    {
        private InstrutorService _instrutorService;

        public InstrutorController(InstrutorService instrutorService)
        {
            _instrutorService = instrutorService;
        }

        [HttpGet("pilar/{pilar}")]
        public IActionResult ListarInstrutoresPorPilar(string pilar) {
            List<ReadInstrutorDto> instrutoresPorPilar = _instrutorService.ListarInstrutorPorPilar(pilar);
            if (instrutoresPorPilar == null) return NotFound();

            return Ok(instrutoresPorPilar);
        }

        [HttpGet]
        public IActionResult ListarInstrutores([FromQuery] string pesquisa, [FromQuery] int page)
        {

            List<ReadInstrutorDto> instrutores = _instrutorService.ListarInstrutores(pesquisa, page);
            if (instrutores == null) return NotFound();
           
            int qtdTotalInstrutores = 0;
            
            if (pesquisa == null || pesquisa == "") {
                qtdTotalInstrutores = _instrutorService.QuantidadeTotalInstrutores();
            } 
            else {
                qtdTotalInstrutores = _instrutorService.QuantidadeTotalPesquisa(pesquisa);
            }
            
            return Ok(new {
                instrutores,
                qtdTotalInstrutores
            });
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarInstrutorPorId(int id)
        {
            ReadInstrutorDto instrutorDto = _instrutorService.RecuperarInstrutorPorId(id);
            if (instrutorDto == null) return NotFound();
            return Ok(instrutorDto);
        }

        [HttpPost]
        public IActionResult CriarInstrutor([FromBody] CreateInstrutorDto createInstrutorDto)
        {
            Instrutor instrutor = _instrutorService.CriarInstrutor(createInstrutorDto);
            return CreatedAtAction(nameof(RecuperarInstrutorPorId), new { Id = instrutor.Id }, instrutor);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarInstrutor(int id, [FromBody] UpdateInstrutorDto updateInstrutorDto)
        {
            Result result = _instrutorService.AtualizarInstrutor(id, updateInstrutorDto);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarInstrutor(int id)
        {
            Result result = _instrutorService.ApagarInstrutor(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }

        [HttpPatch("{id}")]
        public IActionResult RestaurarInstrutorPorID(int id)
        {
            Result result = _instrutorService.RestaurarInstrutorPorID(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }
    }
}
