using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Data.Request;
using API_Calendario_CEC.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        private ReservaService _reservaService;

        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public IActionResult ListaReservas([FromQuery] string data)
        {
            List<ReadReservaDto> reservasDto = _reservaService.ListarReservas(data);
            if(reservasDto == null) return NotFound();
            return Ok(reservasDto);
        }

        [HttpGet]
        [Route("/calendario")]
        public IActionResult ListaReservasCalendario()
        {
            List<FullCalendarRequest> reservasCalendarioDto = _reservaService.ListarReservasCalendario();
            if (reservasCalendarioDto == null) return NotFound();
            return Ok(reservasCalendarioDto);
        }

        [HttpPost]
        public IActionResult CriarReserva([FromBody] CreateReservaDto createReservaDto)
        {
            Result resultado = _reservaService.criaReserva(createReservaDto);
            if (resultado.IsFailed) return BadRequest(resultado.Reasons);
            return Ok(resultado.Reasons);
        }
    }
}
