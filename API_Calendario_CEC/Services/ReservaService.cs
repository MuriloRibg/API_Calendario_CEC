using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Aulas;
using API_Calendario_CEC.Data.Dto.Eventos;
using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Data.Request;
using API_Calendario_CEC.Helps;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class ReservaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        //serviçõs utilizados para validação
        private InstrutorService _instrutorService;
        private TurmaService _turmaService;
        private DisciplinaService _disciplinaService;
        private LocalService _localService;
        private AulaService _aulaService;
        private EventoService _eventoService;

        public ReservaService(AppDbContext context, IMapper mapper, InstrutorService instrutor, DisciplinaService disiciplina, TurmaService turma, AulaService aulaService, EventoService eventoService, LocalService localService)
        {
            _context = context;
            _mapper = mapper;
            _instrutorService = instrutor;
            _turmaService = turma;
            _disciplinaService = disiciplina;
            _aulaService = aulaService;
            _eventoService = eventoService;
            _localService = localService;
        }

        public Result<Object> ListarReservas(string? data=null, string? pesquisa=null, int? page=0, int? idTurma=0)
        {
            List<Reserva> reservas;
            List<ReadReservaDto> reservasDto;
            int qtdTotalReservas;
            Object reservasPorPagina;

            DateTime dataQuery;
            pesquisa = pesquisa?.ToLower();

            if (pesquisa != null && data != null)
            {
                dataQuery = DateTime.Parse(data);
                reservas = _context.Reservas
                    .Where(r => 
                        r.DataInicio.Equals(dataQuery) &&
                        (
                            r.Titulo.ToLower().Contains(pesquisa) ||
                            r.Local.Nome.ToLower().Contains(pesquisa)
                        )
                    )
                    .ToList();

                qtdTotalReservas = reservas.Count();
            }
            else if (idTurma != 0 && data != null)
            {
                dataQuery = DateTime.Parse(data);
                reservas = _context.innerJoinReservaAula("aulas", idTurma)
                            .Where(r => r.DataInicio.Equals(dataQuery))
                            .ToList();

                qtdTotalReservas = reservas.Count();
            }
            else if (pesquisa != null)
            {
                reservas = _context.Reservas
                    .Where(r => 
                        r.Titulo.ToLower().Contains(pesquisa) ||
                        r.Local.Nome.ToLower().Contains(pesquisa)
                    )
                    .ToList();

                qtdTotalReservas = reservas.Count();
            }
            else if (idTurma != 0)
            {
                reservas = _context.innerJoinReservaAula("aulas", idTurma);

                qtdTotalReservas = reservas.Count();
            }
            else if (data != null)
            {
                dataQuery = DateTime.Parse(data);
                reservas = _context.Reservas
                    .Where(r => r.DataInicio.Equals(dataQuery))
                    .ToList();

                qtdTotalReservas = reservas.Count();
            }
            else
            {
                reservas = _context.Reservas.ToList();
                qtdTotalReservas = reservas.Count();
            }

            if (reservas != null && page != null && page != 0)
            {
                int pageSize = 6;
                int currentPage = (page ?? 1);

                reservasDto = _mapper.Map<List<ReadReservaDto>>(reservas.ToPagedList(currentPage, pageSize));

                reservasPorPagina = (new
                {
                    reservasDto,
                    qtdTotalReservas
                });
                return Result.Ok(reservasPorPagina);
            }

            reservasDto = _mapper.Map<List<ReadReservaDto>>(reservas);
            reservasPorPagina = (new
            {
                reservasDto,
                qtdTotalReservas
            });

            return Result.Ok(reservasPorPagina);
        }

        

        public List<FullCalendarRequest> ListarReservasCalendario()
        {
            List<Aula> aulas = _context.Aulas.ToList();
            List<FullCalendarRequest> fullCalendar= new List<FullCalendarRequest>();

            foreach (Aula aula in aulas)
            {
                string data = aula.Reserva.DataInicio.ToString("yyyy-MM-dd");
                string start = data + "T" + aula.Reserva.HoraInicio;
                string end = data + "T" + aula.Reserva.HoraFim;
                string cor = aula.Turma.Pilar.Cor;

                fullCalendar.Add(new FullCalendarRequest(
                    aula.Reserva.Titulo, start, end, cor,
                    aula.Descricao,
                    aula.Instrutor.Nome,
                    aula.Reserva.Local.Nome,
                    aula.Turma.Nome,
                    aula.Disciplina.Nome
                ));
            }

            List<Evento> eventos = _context.Eventos.ToList();

            foreach (Evento evento in eventos)
            {
                string data = evento.Reserva.DataInicio.ToString("yyyy-MM-dd");
                string start = data + "T" + evento.Reserva.HoraInicio;
                string end = data + "T" + evento.Reserva.HoraFim;
                string cor = "";

                fullCalendar.Add(new FullCalendarRequest(
                    evento.Reserva.Titulo, start, end, cor,
                    evento.Descricao, evento.Instrutor.Nome, evento.Reserva.Local.Nome
                    ));
            }

            return fullCalendar;


        }

        public Result AtualizaReserva(UpdateReservaDto reservaUpdate, int id)
        {
            //Validando os horários
            bool resultValidaHorario = ValidacaoHelp.ValidarHorario(
                reservaUpdate.HoraInicio, reservaUpdate.HoraFim
                );

            if (resultValidaHorario == true)
            {
                return Result.Fail("Hora inicio é maior que hora fim!");
            }


            List<ValidacaoRequest> validacao = new List<ValidacaoRequest>();

            Reserva reserva = _context.Reservas.FirstOrDefault(reserva => reserva.Id == id);
            
            if (reserva == null) {
                Result.Fail("Reserva não encontrada!");
            }
            
            _mapper.Map(reservaUpdate, reserva);

            if (reservaUpdate.TipoEvento.ToLower() == "aula") {
                
                UpdateAulaDto aulaUpdate = new UpdateAulaDto(
                   reservaUpdate.Id_Aula,
                   reservaUpdate.Id_Instrutor,
                   reservaUpdate.Id_Turma,
                   reservaUpdate.Id_Disciplina,
                   reservaUpdate.Descricao
                );

                Reserva validaLocalAula = _context
                    .validaEvento(
                        id,
                        "aulas",
                        "Id_local",
                        reservaUpdate.Id_Local,
                        reservaUpdate.DataInicio.ToString("yyyy-MM-dd"),
                        reservaUpdate.HoraInicio, reservaUpdate.HoraFim
                    );

                Reserva validaInstrutor = _context.validaEvento(
                    id, 
                    "aulas", 
                    "Id_instrutor", 
                    aulaUpdate.Id_Instrutor, 
                    reservaUpdate.DataInicio.ToString("yyyy-MM-dd"), 
                    reservaUpdate.HoraInicio, reservaUpdate.HoraFim
                );

                Reserva validaTurma = _context.validaEvento(
                    id,
                    "aulas",
                    "Id_turma",
                    aulaUpdate.Id_Turma,
                    reservaUpdate.DataInicio.ToString("yyyy-MM-dd"), 
                    reservaUpdate.HoraInicio, reservaUpdate.HoraFim
                );

                validacao.Add(new ValidacaoRequest(validaLocalAula != null, "Local ocupado neste horário"));
                validacao.Add(new ValidacaoRequest(validaInstrutor != null, "Instrutor ocupado neste horário"));
                validacao.Add(new ValidacaoRequest(validaTurma != null, "Turma ocupada neste horário"));

                List<string> erros = validacao.FindAll(e => e.Validacao == true).Select(e => e.Message).ToList();

                if (erros.Count != 0)
                {
                    return Result.Ok().WithErrors(erros);
                }
                
                //salva a reserva antes de começar a atualiza a aula relacionada
                _context.SaveChanges();
                
                Result resultado = _aulaService.AtualizaAula(aulaUpdate, aulaUpdate.Id);
                if (resultado.IsFailed) {
                    return Result.Fail("Aula não encontrada!");
                }
            }
            else if (reservaUpdate.TipoEvento.ToLower() == "evento")
            {
                UpdateEventoDto eventoDto = new UpdateEventoDto(
                    reservaUpdate.Id_Instrutor,
                    reservaUpdate.Id_Evento
                );

                Reserva validaLocalEvento = _context
                .validaEvento(
                    id,
                    "eventos",
                    "Id_local",
                    reservaUpdate.Id_Local,
                    reservaUpdate.DataInicio.ToString("yyyy-MM-dd"),
                    reservaUpdate.HoraInicio, reservaUpdate.HoraFim
                );

                validacao.Add(new ValidacaoRequest(validaLocalEvento != null, "Local ocupado neste horário"));

                List<string> erros = validacao.FindAll(e => e.Validacao == true).Select(e => e.Message).ToList();

                if (erros.Count != 0)
                {
                    return Result.Ok().WithErrors(erros);
                }

                //salva a reserva antes de começar a atualiza a aula relacionada
                _context.SaveChanges();

                Result resultado = _eventoService.AtualizaEvento(eventoDto, eventoDto.Id);
                if (resultado.IsFailed)
                {
                    return Result.Fail("Evento não encontrada!");
                }

            }

            return Result.Ok().WithSuccess("Adicionado com sucesso!");
        }

        public Result criaReserva(CreateReservaDto createReservaDto)
        {
            //Validando os horários
            bool resultValidaHorario = ValidacaoHelp.ValidarHorario(
                createReservaDto.HoraInicio, createReservaDto.HoraFim
                );

            if (resultValidaHorario == true)
            {
                return Result.Fail("Hora inicio é maior que hora fim!");
            }

            List<ValidacaoRequest> validacao = new List<ValidacaoRequest>();

            //verifica se local existe
            bool local = _localService.RecuperarLocalPorId(createReservaDto.Id_Local) == null;    
            validacao.Add(new ValidacaoRequest(local, "Local não existe"));

            Reserva reserva = _mapper.Map<Reserva>(createReservaDto);
            _context.Reservas.Add(reserva);
            Console.WriteLine(reserva.Id_Local);

            if (createReservaDto.TipoEvento.ToLower() == "aula")
            {
                Reserva validaLocal = _context
               .validaEvento(
                   0,
                   "aulas",
                   "Id_local",
                   createReservaDto.Id_Local,
                   createReservaDto.DataInicio.ToString("yyyy-MM-dd"),
                   createReservaDto.HoraInicio, createReservaDto.HoraFim
               );
                Reserva validaTurma = _context
                .validaEvento(
                    0,
                    "aulas",
                    "Id_turma",
                    createReservaDto.Id_Turma,
                    createReservaDto.DataInicio.ToString("yyyy-MM-dd"),
                    createReservaDto.HoraInicio, createReservaDto.HoraFim
                );
                Reserva validaInstrutor = _context
                .validaEvento(
                    0,
                    "aulas",
                    "Id_instrutor",
                    createReservaDto.Id_Instrutor,
                    createReservaDto.DataInicio.ToString("yyyy-MM-dd"),
                    createReservaDto.HoraInicio, createReservaDto.HoraFim
                );
                bool instrutor = _instrutorService.RecuperarInstrutorPorId(createReservaDto.Id_Instrutor) == null;
                bool turma = _turmaService.RecuperarTurmaPorId(createReservaDto.Id_Turma) == null;
                bool disciplina = _disciplinaService.RecuperarDisciplinaPorId(createReservaDto.Id_Disciplina) == null;

                validacao.Add(new ValidacaoRequest(validaLocal != null, "Local ocupado neste horário"));
                validacao.Add(new ValidacaoRequest(validaInstrutor != null, "Instrutor ocupado neste horário"));
                validacao.Add(new ValidacaoRequest(validaTurma != null, "Turma ocupada neste horário"));
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
                   createReservaDto.Descricao,
                   reserva.Id
                );

                Aula aula = _mapper.Map<Aula>(aulaDto);
                _context.Aulas.Add(aula);
                _context.SaveChanges();
            }
            else if(createReservaDto.TipoEvento.ToLower() == "evento")
            {
                Reserva validaLocal = _context
                  .validaEvento(
                      0,
                      "eventos",
                      "Id_local",
                      createReservaDto.Id_Local,
                      createReservaDto.DataInicio.ToString("yyyy-MM-dd"),
                      createReservaDto.HoraInicio, createReservaDto.HoraFim
                  );
                validacao.Add(new ValidacaoRequest(validaLocal != null, "Local ocupado neste horário"));

                List<string> erros = validacao.FindAll(e => e.Validacao == true).Select(e => e.Message).ToList();

                if (erros.Count != 0)
                {
                    return Result.Ok().WithErrors(erros);
                }

                //salva a reserva antes de começar a criar a aula relacionada
                _context.SaveChanges();

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
        
        public Result DeletaReserva(int idReserva)
        {
            Reserva reserva = _context.Reservas
                .FirstOrDefault(reserva => reserva.Id == idReserva);
            if(reserva == null) return Result.Fail("Reserva não encontrada!");

            Result resultadoDeleteAula = _aulaService.DeletaAula(idReserva);
            Result resultadoDeleteEvento = _eventoService.DeletaEvento(idReserva);

            if (resultadoDeleteAula.IsFailed && resultadoDeleteEvento.IsFailed) return Result.Fail("Erro ao excluir reserva!");

            _context.Remove(reserva);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Reserva excluida!");

        }
        
    }
}
