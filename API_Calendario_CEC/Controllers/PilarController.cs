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
        public IActionResult RetornaPilares()
        {
            List<string> pilaresDto = _pilarService.RetornaPilar();
            if (pilaresDto == null) return NotFound();
            return Ok(pilaresDto);
        }

        [HttpGet]
        public IActionResult RetornaTodosPilares()
        {
            List<ReadPilarDto> pilaresDto = _pilarService.RetornaTodosPilares();
            if (pilaresDto == null) return NotFound();
            return Ok(pilaresDto);
        }

        [HttpGet("{id}")]
        public IActionResult RetornaPilarPorId(int id)
        {
            ReadPilarDto readPilarDto = _pilarService.RetornaPilarPorId(id);
            if (readPilarDto == null) return NotFound();
            return Ok(readPilarDto);
        }

        [HttpPost]
        public IActionResult CriarPilar(CreatePilarDto createPilarDto)
        {
            Pilar pilar = _pilarService.CriarPilar(createPilarDto);
            return CreatedAtAction(nameof(RetornaPilarPorId), new { Id = pilar.Id }, pilar);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarPilar(int id, [FromBody] UpdatePilarDto updatePilarDto)
        {
            Result result = _pilarService.AtualizarPilar(id, updatePilarDto);
            if (result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarPilar(int id)
        {
            Result result = _pilarService.DeletarPilar(id);
            if(result.IsFailed) return NotFound(result.Reasons);
            return Ok(result.Reasons);
        }
    }
}
