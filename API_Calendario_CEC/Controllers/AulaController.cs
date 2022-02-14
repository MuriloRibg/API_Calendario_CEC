using API_Calendario_CEC.Data.Dto.Aulas;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Calendario_CEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AulaController : ControllerBase
    {
        private AulaService _aulaService;

        public AulaController(AulaService aulaService)
        {
            _aulaService = aulaService;
        }

       [HttpGet]
       public IActionResult ListaAulas()
        {
            List<ReadAulaDto> aulas = _aulaService.ListarAulas();
            if(aulas == null) return NotFound();
            return Ok(aulas);
        }

        [HttpPost]
        public IActionResult CriarAula([FromBody] CreateAulaDto createAulaDto)
        {
            ReadAulaDto aula = _aulaService.CriarAula(createAulaDto);
            return Ok(aula);
        }
    }
}
