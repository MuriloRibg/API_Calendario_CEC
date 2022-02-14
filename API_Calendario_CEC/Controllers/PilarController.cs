using API_Calendario_CEC.Data.Dto.Pilares;
using API_Calendario_CEC.Services;
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
            if(pilaresDto == null) return NotFound();
            return Ok(pilaresDto);
        }

        [HttpGet]
        public IActionResult retornaTodosPilares()
        {
            List<ReadPilarDto> pilaresDto = _pilarService.retornaTodosPilares();
            if (pilaresDto == null) return NotFound();
            return Ok(pilaresDto);
        }

    }
}
