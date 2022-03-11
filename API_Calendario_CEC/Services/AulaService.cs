using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto.Aulas;
using API_Calendario_CEC.Data.Dto.Reservas;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace API_Calendario_CEC.Services
{
    public class AulaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AulaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ReadAulaDto> ListarAulas()
        {
            List<Aula> aulas = _context.Aulas.ToList();
            return _mapper.Map<List<ReadAulaDto>>(aulas);
        }

        public ReadAulaDto CriarAula(CreateAulaDto createAulaDto)
        {
            Aula aula = _mapper.Map<Aula>(createAulaDto);
            _context.Add(aula);
            _context.SaveChanges();
            return _mapper.Map<ReadAulaDto>(aula);
        }

        public Result AtualizaAula(UpdateAulaDto updateAulaDto, int id) {
            Aula aula = _context.Aulas.FirstOrDefault(aula => aula.Id == id);
                
            if (aula == null) {
                return Result.Fail("Aula não encontrada!");
            }
            
            _mapper.Map(updateAulaDto, aula);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaAula(int idAula)
        {
            Aula aula = _context.Aulas
                .FirstOrDefault(aula => aula.Id == idAula);

            if (aula == null) return Result.Fail("Aula não encontrada!");

            _context.Remove(aula);
            _context.SaveChanges();
            return Result.Ok();

        }

    }
}
