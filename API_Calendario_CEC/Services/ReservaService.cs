using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Data.Request;
using API_Calendario_CEC.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class ReservaService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private List<FullCalendarRequest> fullCalendarRequests;

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

        public Reserva criaReserva(CreateReservaDto createReservaDto)
        {
            Reserva reserva = _mapper.Map<Reserva>(createReservaDto);
            _context.Add(reserva);
            _context.SaveChanges();
            return reserva;
        }        
    }
}
