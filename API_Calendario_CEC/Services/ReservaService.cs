﻿using API_Calendario_CEC.Data;
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
            Reserva valida = _context.validaReserva(0, "Id_local", createReservaDto.Id_Local ,createReservaDto.DataInicio.ToString("yyyy-MM-dd") , createReservaDto.HoraInicio, createReservaDto.HoraFim);
            if (valida != null) return Result.Fail("Falhou");
            Reserva reserva = _mapper.Map<Reserva>(createReservaDto);
            _context.Add(reserva);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Adicionado");
        }        
    }
}
