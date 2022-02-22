using API_Calendario_CEC.Data.Dto.Pilares;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PilarController : ControllerBase
    {
        private PilarService _pilarService;

        public PilarController(PilarService pilarService)
        {
            _pilarService = pilarService;
        }

        [HttpGet]
        [Route("pilares")]
        public IActionResult retornaPilares()
        {
            List<string> pilaresDto = _pilarService.retornaPilar();
            if (pilaresDto == null) return NotFound();
            return Ok(pilaresDto);
        }

        [HttpGet]
        public IActionResult retornaTodosPilares()
        {
            List<ReadPilarDto> pilaresDto = _pilarService.retornaTodosPilares();
            if (pilaresDto == null) return NotFound();
            return Ok(pilaresDto);
        }

        [HttpGet("{id}")]
        public IActionResult retornaPilarPorId(int id)
        {
            ReadPilarDto readPilarDto = _pilarService.retornaPilarPorId(id);
            if (readPilarDto == null) return NotFound();
            return Ok(readPilarDto);
        }

        [HttpPost]
        public IActionResult criarPilar(CreatePilarDto createPilarDto)
        {
            Pilar pilar = _pilarService.criarPilar(createPilarDto);
            return CreatedAtAction(nameof(retornaPilarPorId), new { Id = pilar.Id }, pilar);
        }

        [HttpPut("{id}")]
        public IActionResult atualizarPilar(int id, [FromBody] UpdatePilarDto updatePilarDto)
        {
            Result result = _pilarService.atualizarPilar(id, updatePilarDto);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }

        [HttpDelete("{id}")]
        public IActionResult deletarPilar(int id)
        {
            Result result = _pilarService.deletarPilar(id);
            if(result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }
    }
}
