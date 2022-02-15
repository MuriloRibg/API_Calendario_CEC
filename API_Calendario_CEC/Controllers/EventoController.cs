using API_Calendario_CEC.Data.Dto.Eventos;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private EventoService _eventoService;
        public EventoController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public IActionResult RetornaEventos()
        {
            List<ReadEventoDto> eventosDto = _eventoService.ListaEventos();
            if(eventosDto == null) return NotFound();
            return Ok(eventosDto);
        }

        [HttpPost]
        public IActionResult CriaEventos([FromBody] CreateEventoDto createEventoDto)
        {
            ReadEventoDto eventoDto = _eventoService.CriaEvento(createEventoDto);
            return Ok(eventoDto);
        }
    }
}
