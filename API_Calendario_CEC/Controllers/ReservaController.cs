using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Models;
using API_Calendario_CEC.Services;
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
        public IActionResult ListaReservas()
        {
            List<ReadReservaDto> reservasDto = _reservaService.ListarReservas();
            if(reservasDto == null) return NotFound();
            return Ok(reservasDto);
        }

        [HttpPost]
        public IActionResult CriarReserva([FromBody] CreateReservaDto createReservaDto)
        {
            Reserva reserva = _reservaService.criaReserva(createReservaDto);
            return Ok(reserva);
        }
    }
}
