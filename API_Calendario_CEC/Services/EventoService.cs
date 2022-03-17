using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Eventos;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
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

        public Result AtualizaEvento(UpdateEventoDto updateEventoDto, int idEvento)
        {
            Evento evento = _context.Eventos.FirstOrDefault(evento => evento.Id == idEvento);

            if (evento == null)
            {
                return Result.Fail("Evento não encontrada!");
            }

            _mapper.Map(updateEventoDto, evento);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaEvento(int idReserva)
        {
            Evento evento = _context.Eventos
                .FirstOrDefault(evento => evento.Id_Reserva == idReserva);

            if (evento == null) return Result.Fail("Evento não encontrada!");

            _context.Remove(evento);
            _context.SaveChanges();
            return Result.Ok();

        }
    }
}
