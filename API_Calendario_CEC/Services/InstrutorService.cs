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
        public List<ReadInstrutorDto> listarInstrutores()
        {
            List<Instrutor> instrutores = _context.Instrutores.ToList();
            if (instrutores == null) return null;
            return _mapper.Map<List<ReadInstrutorDto>>(instrutores);
        }

        //GET ID
        public ReadInstrutorDto recuperarInstrutorPorId(int id)
        {
            Instrutor instrutor = _context.Instrutores.FirstOrDefault(instrutor => instrutor.Id == id);
            if (instrutor == null) return null;
            return _mapper.Map<ReadInstrutorDto>(instrutor); 
        }


        //POST
        public Instrutor criarInstrutor(CreateInstrutorDto createInstrutorDto)
        {
            Instrutor instrutor = _mapper.Map<Instrutor>(createInstrutorDto);
            _context.Add(instrutor);
            _context.SaveChanges();
            return instrutor;
        }

        //PUT
        public Result atualizarInstrutor(int id, UpdateInstrutorDto updateInstrutorDto)
        {
            Instrutor instrutor = _context.Instrutores.FirstOrDefault(instrutor => instrutor.Id == id);
            if (instrutor == null) return Result.Fail("Instrutor não encontrado");
            _mapper.Map(updateInstrutorDto, instrutor); //Atualizando o instrutor;
            _context.SaveChanges();
            return Result.Ok();
        }

        //DELETE
        public Result apagarInstrutor(int id)
        {
            Instrutor instrutor = _context.Instrutores.FirstOrDefault(instrutor => instrutor.Id == id);
            if (instrutor == null) return Result.Fail("Instrutor não encontrado!");
            _context.Remove(instrutor);
            _context.SaveChanges();
            return Result.Ok();
        }        
    }
}
