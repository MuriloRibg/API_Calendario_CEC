using System;
using System.Collections.Generic;
using System.Linq;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;


namespace API_Calendario_CEC.Services
{
    public class InstrutorService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public InstrutorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET
        public List<ReadInstrutorDto> ListarInstrutores()
        {
            List<Instrutor> instrutores = _context.Instrutores.ToList();
            if (instrutores == null) return null;
            return _mapper.Map<List<ReadInstrutorDto>>(instrutores);
        }

        //GET ID
        public ReadInstrutorDto RecuperarInstrutorPorId(int id)
        {
            Instrutor instrutor = _context.Instrutores.FirstOrDefault(instrutor => instrutor.Id == id);
            if (instrutor == null) return null;
            return _mapper.Map<ReadInstrutorDto>(instrutor); 
        }


        //POST
        public Instrutor CriarInstrutor(CreateInstrutorDto createInstrutorDto)
        {
            Instrutor instrutorCadastrado = _context
                .Instrutores
                .FirstOrDefault(instrutor => instrutor.Email.ToUpper() == createInstrutorDto.Email.ToUpper());
            if(instrutorCadastrado == null)
            {
                Instrutor instrutor = _mapper.Map<Instrutor>(createInstrutorDto);

                _context.Add(instrutor);    
                _context.SaveChanges();
                return instrutor;
            }
            return null;
        }

        //PUT
        public Result AtualizarInstrutor(int id, UpdateInstrutorDto updateInstrutorDto)
        {
            Instrutor instrutor = _context.Instrutores.FirstOrDefault(instrutor => instrutor.Id == id);
            if (instrutor == null) return Result.Fail("Instrutor não encontrado");
            _mapper.Map(updateInstrutorDto, instrutor); //Atualizando o instrutor;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Instrutor atualizado com sucesso!");
        }

        //DELETE
        public Result ApagarInstrutor(int id)
        {
            Instrutor instrutor = _context.Instrutores.FirstOrDefault(instrutor => instrutor.Id == id);
            if (instrutor == null) return Result.Fail("Instrutor não encontrado!");
            _context.Remove(instrutor);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Instrutor apagado com sucesso!");
        }        
    }
}
