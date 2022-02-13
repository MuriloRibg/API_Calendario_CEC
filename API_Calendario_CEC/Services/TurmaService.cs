using System;
using System.Collections.Generic;
using System.Linq;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Turmas;
using API_Calendario_CEC.Models;
using AutoMapper;

namespace API_Calendario_CEC.Services
{
    public class TurmaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public TurmaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET
        public List<ReadTurmasDto> ListarTurma()
        {
            List<Turma> turmas = _context.Turmas
                .Where(turma => turma.DeleteAt == null)
                .ToList();
            if (turmas == null) return null;
            return _mapper.Map<List<ReadTurmasDto>>(turmas);
        }
    }
}
