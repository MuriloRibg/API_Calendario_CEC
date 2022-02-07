﻿using System.Collections.Generic;
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
        public IActionResult ListarInstrutores()
        {
            List<ReadInstrutorDto> instrutores = _instrutorService.ListarInstrutores();
            if (instrutores == null) return NotFound();
            return Ok(instrutores);
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
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarInstrutor(int id)
        {
            Result result = _instrutorService.ApagarInstrutor(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok();
        }

    }
}
