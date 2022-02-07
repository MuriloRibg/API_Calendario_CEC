using System;
using System.Collections.Generic;
using API_Calendario_CEC.Data.Dto.Turmas;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class TurmaController : ControllerBase
    {
        private TurmaService _turmaService;

        public TurmaController(TurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet]
        public IActionResult ListarTurma()
        {
            List<ReadTurmasDto> turmasDto = _turmaService.ListarTurma();
            if (turmasDto == null) return NotFound();
            return Ok(turmasDto);
        }
    }
}
