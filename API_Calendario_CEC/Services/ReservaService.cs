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

        //serviçõs utilizados para validação
        private InstrutorService _instrutorService;
        private TurmaService _turmaService;
        private DisciplinaService _disciplinaService;

        public ReservaService(AppDbContext context, IMapper mapper, InstrutorService instrutor, DisciplinaService disiciplina, TurmaService turma)
        {
            _context = context;
            _mapper = mapper;
            _instrutorService = instrutor;
            _turmaService = turma;
            _disciplinaService = disiciplina;
        }

        public List<ReadReservaDto> ListarReservas()
        {
            List<Reserva> reservas = _context.Reservas.ToList();
            return _mapper.Map<List<ReadReservaDto>>(reservas);
        }

        public List<FullCalendarRequest> ListarReservasCalendario()
        {
            List<Reserva> reservas = _context.Reservas.ToList();
            List<FullCalendarRequest> fullCalendar= new List<FullCalendarRequest>();

            foreach (Reserva reserva in reservas)
            {
                string data = reserva.DataInicio.ToString("yyyy-MM-dd");
                string start = data + "T" + reserva.HoraInicio;
                string end = data + "T" + reserva.HoraFim;
                string cor = reserva.Aula.Turma.Pilar.Cor;
                string display = "block";

                fullCalendar.Add(new FullCalendarRequest(reserva.Titulo, start, end, cor, display));
            }
            return fullCalendar;
        }

        public Result criaReserva(CreateReservaDto createReservaDto)
        {
            Reserva validaInstrutor = _context
                .validaEvento(
                    0, 
                    "aulas", 
                    "Id_instrutor", 
                    createReservaDto.Id_Instrutor, 
                    createReservaDto.DataInicio.ToString("yyyy-MM-dd"), 
                    createReservaDto.HoraInicio, createReservaDto.HoraFim
                );
            Reserva validaLocal = _context
                .validaEvento(
                    0, 
                    "aulas", 
                    "Id_local", 
                    createReservaDto.Id_Local,
                    createReservaDto.DataInicio.ToString("yyyy-MM-dd"), 
                    createReservaDto.HoraInicio, createReservaDto.HoraFim
                );    
                
            if (
                validaInstrutor != null || 
                validaLocal != null
            ) return Result.Fail("Falhou");

            Reserva reserva = _mapper.Map<Reserva>(createReservaDto);
            _context.Reservas.Add(reserva);

            if (createReservaDto.Id_Turma != 0)
            {
                var instrutor = _instrutorService.RecuperarInstrutorPorId(createReservaDto.Id_Instrutor) == null;
                var turma = _turmaService.RecuperarTurmaPorId(createReservaDto.Id_Turma) == null;
                var disciplina = _disciplinaService.RecuperarDisciplinaPorId(createReservaDto.Id_Disciplina) == null;

                List<ValidacaoRequest> validacao = new List<ValidacaoRequest>();
                validacao.Add(new ValidacaoRequest(instrutor, "Instrutor não existe"));
                validacao.Add(new ValidacaoRequest(turma, "Turma não existe"));
                validacao.Add(new ValidacaoRequest(disciplina, "Disciplina não existe"));

                List<string> erros = validacao.FindAll(e => e.Validacao == true).Select(e => e.Message).ToList();

                if (erros.Count != 0)
                {
                    return Result.Ok().WithErrors(erros);
                }

                //salva a reserva antes de começar a criar a aula relacionada
                _context.SaveChanges();

                CreateAulaDto aulaDto = new CreateAulaDto(
                   createReservaDto.Id_Instrutor,
                   createReservaDto.Id_Turma,
                   createReservaDto.Id_Disciplina,
                   reserva.Id
                );

                Aula aula = _mapper.Map<Aula>(aulaDto);
                _context.Aulas.Add(aula);
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
            return Result.Ok().WithSuccess("Adicionado com sucesso!");
        }        
    }
}
