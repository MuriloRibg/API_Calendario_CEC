using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Pilares;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class PilarService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public PilarService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET
        public List<string> RetornaPilar()
        {
            IEnumerable<Pilar> queryLastNames = ( from Pilares in _context.Pilares
                                                select Pilares).Distinct().ToList();

            Console.WriteLine(queryLastNames);
            List<string> pilares = _context.Pilares.Select(pilar => pilar.NomePilar).Distinct().ToList();
            if (pilares == null) return null;
            return pilares;
        }

        //GET
        public List<ReadPilarDto> RetornaTodosPilares()
        {
            List<Pilar> pilares = _context.Pilares.ToList();
            if (pilares == null) return null;
            return _mapper.Map<List<ReadPilarDto>>(pilares);
        }

        //GET ID
        public ReadPilarDto RetornaPilarPorId(int id)
        {
            Pilar pilar = _context.Pilares.FirstOrDefault(pilar => pilar.Id == id);
            if (pilar == null) return null;
            return _mapper.Map<ReadPilarDto>(pilar);
        }

        public List<ReadPilarDto> RetornaCategoriasPorPilar(string pilar)
        {
            List<Pilar> pilares = _context.Pilares
                .Where(p => p.NomePilar.ToUpper() == pilar.ToUpper()).ToList();
            if (pilares == null) return null;
            return _mapper.Map<List<ReadPilarDto>>(pilares);
        }

        //POST
        public Pilar CriarPilar(CreatePilarDto createPilarDto)
        {
            Pilar pilar = _context.Pilares
                .FirstOrDefault(pilar => pilar.NomePilar.ToUpper() == createPilarDto.NomePilar.ToUpper());
            if(pilar == null)
            {
                Pilar novoPilar = _mapper.Map<Pilar>(createPilarDto);
                _context.Add(novoPilar);
                _context.SaveChanges();
                return novoPilar;
            }
            return null;
        }

        //PUT
        public Result AtualizarPilar(int id, UpdatePilarDto updatePilarDto)
        {
            Pilar pilar = _context.Pilares.FirstOrDefault(pilar => pilar.Id == id);
            if (pilar == null) return Result.Fail("Pilar não encontrado!");
            _mapper.Map(updatePilarDto, pilar);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Pilar atualizado com sucesso!");
        }

        //DELETE
        public Result DeletarPilar(int id)
        {
            Pilar pilar = _context.Pilares.FirstOrDefault(pilar => pilar.Id == id);
            if (pilar == null) return Result.Fail("Pilar não encontrado!");
            pilar.DeleteAt = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Pilar apagado com sucesso!");
        }

    }
}
