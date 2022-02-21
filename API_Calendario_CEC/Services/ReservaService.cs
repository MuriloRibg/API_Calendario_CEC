using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Aulas;
using API_Calendario_CEC.Data.Dto.Eventos;
using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Data.Request;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class ReservaService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public List<FullCalendarRequest> fullCalendarRequests;

        public ReservaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ReadReservaDto> ListarReservas()
        {
            List<Reserva> reservas = _context.Reservas.ToList();
            return _mapper.Map<List<ReadReservaDto>>(reservas);
        }

        public Result criaReserva(CreateReservaDto createReservaDto)
        {
            Reserva validaInstrutor = _context
                .validaEvento(0, "aulas", "Id_instrutor", createReservaDto.Id_Instrutor ,createReservaDto.DataInicio.ToString("yyyy-MM-dd") , createReservaDto.HoraInicio, createReservaDto.HoraFim);
            Reserva validaLocal = _context
                .validaEvento(0, "aulas", "Id_local", createReservaDto.Id_Local ,createReservaDto.DataInicio.ToString("yyyy-MM-dd") , createReservaDto.HoraInicio, createReservaDto.HoraFim);    
                
            if (
                validaInstrutor != null || 
                validaLocal != null
            ) return Result.Fail("Falhou");

            Reserva reserva = _mapper.Map<Reserva>(createReservaDto);
            _context.Add(reserva);
            _context.SaveChanges();

            if (createReservaDto.Id_Turma != 0)
            {
                CreateAulaDto aulaDto = new CreateAulaDto(
                   createReservaDto.Id_Instrutor,
                   createReservaDto.Id_Turma,
                   createReservaDto.Id_Disciplina,
                   reserva.Id
                );

                Aula aula = _mapper.Map<Aula>(aulaDto);
                _context.Add(aula);
                _context.SaveChanges();
            }
            else if(createReservaDto.Descricao.Length != 0)
            {
                CreateEventoDto eventoDto = new CreateEventoDto(
                    createReservaDto.Id_Instrutor,
                    createReservaDto.Descricao,
                    reserva.Id
                );

                Evento evento = _mapper.Map<Evento>(eventoDto);
                _context.Add(evento);
                _context.SaveChanges();
            }
            return Result.Ok().WithSuccess("Adicionado");
        }        
    }
}
