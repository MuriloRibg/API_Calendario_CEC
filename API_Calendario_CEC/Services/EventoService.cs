using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Eventos;
using API_Calendario_CEC.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class EventoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EventoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ReadEventoDto> ListaEventos()
        {
            List<Evento> eventos = _context.Eventos.ToList();
            return _mapper.Map<List<ReadEventoDto>>(eventos);
        }

        public ReadEventoDto CriaEvento(CreateEventoDto createEventosDto)
        {
            Evento evento = _mapper.Map<Evento>(createEventosDto);
            _context.Eventos.Add(evento);
            _context.SaveChanges();
            return _mapper.Map<ReadEventoDto>(evento);
        }
    }
}
