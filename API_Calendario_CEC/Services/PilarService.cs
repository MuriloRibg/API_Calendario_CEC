using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Pilares;
using API_Calendario_CEC.Models;
using AutoMapper;
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
        public List<string> retornaPilar()
        {
            IEnumerable<Pilar> queryLastNames = ( from Pilares in _context.Pilares
                                                select Pilares).Distinct().ToList();

            Console.WriteLine(queryLastNames);
            List<string> pilares = _context.Pilares.Select(pilar => pilar.NomePilar).Distinct().ToList();
            if (pilares == null) return null;
            return pilares;
        }

        //GET
        public List<ReadPilarDto> retornaTodosPilares()
        {
            List<Pilar> pilares = _context.Pilares.ToList();
            if (pilares == null) return null;
            return _mapper.Map<List<ReadPilarDto>>(pilares);
        }
        //GET ID

        //POST

        //PUT

        //DELETE
    }
}
