using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Aulas;
using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class AulaService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private ReservaService _reservaService;

        public AulaService(AppDbContext context, IMapper mapper,ReservaService reservaService)
        {
            _reservaService = reservaService;
            _context = context;
            _mapper = mapper;
        }

        public List<ReadAulaDto> ListarAulas()
        {
            List<Aula> aulas = _context.Aulas.ToList();
            return _mapper.Map<List<ReadAulaDto>>(aulas);
        }

        public ReadAulaDto CriarAula(CreateAulaDto createAulaDto)
        {
            Aula aula = _mapper.Map<Aula>(createAulaDto);
            _context.Add(aula);
            _context.SaveChanges();
            return _mapper.Map<ReadAulaDto>(aula);
        }

    }
}
