using System.Collections.Generic;
using API_Calendario_CEC.Data.Dto.Locais;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;
using FluentResults;

namespace API_Calendario_CEC.Controllers {

    [ApiController]
    [Route("[controller]")]

    public class LocalController : ControllerBase {

        private LocalService _localService;
        
        public LocalController(LocalService localService) {
            _localService = localService;
        }

        [HttpGet]
        public IActionResult ListarLocais([FromQuery] int capacidade) {
            List<ReadLocaisDto> locais = _localService.ListarLocais(capacidade);
            if (locais == null) return NotFound();
            return Ok(locais);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarLocalPorId(int id) {
            ReadLocaisDto localDto = _localService.RecuperarLocalPorId(id);
            if (localDto == null) return NotFound();
            return Ok(localDto);
        }

        [HttpPost]
        public IActionResult CriarLocal([FromBody] CreateLocaisDto createLocalDto) {
            Local local = _localService.CriarLocal(createLocalDto);
            return CreatedAtAction(nameof(RecuperarLocalPorId), new { Id = local.Id }, local);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarLocal(int id, [FromBody] UpdateLocaisDto updatelocalDto) {
            Result result = _localService.AtualizarLocal(id, updatelocalDto);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarLocal(int id) {
            Result result = _localService.ApagarLocal(id);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok();
        }

    }
}