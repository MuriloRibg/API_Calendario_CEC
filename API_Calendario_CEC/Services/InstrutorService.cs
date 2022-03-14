using System;
using System.Collections.Generic;
using System.Linq;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Data.Dto;
using API_Calendario_CEC.Models;
using AutoMapper;
using FluentResults;

using PagedList;

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

        // Retorna quantidade total de instrutores
        public int QuantidadeTotalInstrutores() {
            int total = _context.Instrutores.Where(instrutor => instrutor.DeleteAt == null).Count();
            return total;
        }

        public int QuantidadeTotalPesquisa(string pesquisa) {
            return _context.Instrutores
                .Where(instrutor =>
                    instrutor.DeleteAt == null &&
                        (
                            instrutor.Nome.ToLower().Contains(pesquisa) ||
                            instrutor.Abreviacao.ToLower().Contains(pesquisa) ||
                            instrutor.Email.ToLower().Contains(pesquisa) ||
                            instrutor.Pilar.ToLower().Contains(pesquisa) ||
                            instrutor.Disponibilidade.ToLower().Contains(pesquisa)
                        )
                )
                .Count();
        }

        public List<ReadInstrutorDto> ListarInstrutores(string? pesquisa, int? page)
        {
            List<Instrutor> instrutores;
            if (pesquisa == null || pesquisa == "")
            {
                instrutores = _context.Instrutores
                    .Where(instrutor => instrutor.DeleteAt == null)
                    .ToList();
            }
            else
            {
                pesquisa = pesquisa.ToLower();
            
                instrutores = _context.Instrutores
                    .Where(instrutor =>
                        instrutor.DeleteAt == null &&
                        (
                            instrutor.Nome.ToLower().Contains(pesquisa) ||
                            instrutor.Abreviacao.ToLower().Contains(pesquisa) ||
                            instrutor.Email.ToLower().Contains(pesquisa) ||
                            instrutor.Pilar.ToLower().Contains(pesquisa) ||
                            instrutor.Disponibilidade.ToLower().Contains(pesquisa)
                        ) 
                    )
                    .ToList();
            }
            if (instrutores != null && page != null && page != 0)
            {
                int pageSize = 6;
                int currentPage = (page ?? 1);
                return _mapper.Map<List<ReadInstrutorDto>>(instrutores.ToPagedList(currentPage, pageSize));
            }

            return _mapper.Map<List<ReadInstrutorDto>>(instrutores);    
        }

        public Result ValidarEmailInstrutor(string email)
        {
            Instrutor instrutor = _context.Instrutores
                .FirstOrDefault(instrutor => instrutor.DeleteAt == null &&
                   instrutor.Email.ToUpper() == email.ToUpper()
                );
            if (instrutor == null) return Result.Ok();
            return Result.Fail("Email em uso!");
        }

        public List<ReadInstrutorDto> ListarInstrutorPorPilar(string pilar) {
            List<Instrutor> instrutores = _context.Instrutores
                .Where(instrutor => instrutor.DeleteAt == null &&
                    instrutor.Pilar.ToUpper() == pilar.ToUpper())
                .ToList();
            return _mapper.Map<List<ReadInstrutorDto>>(instrutores);
        }

        //GET ID
        public ReadInstrutorDto RecuperarInstrutorPorId(int id)
        {
            Instrutor instrutor = _context.Instrutores
                .FirstOrDefault(instrutor => instrutor.Id == id && instrutor.DeleteAt == null);

            if (instrutor == null) return null;
            return _mapper.Map<ReadInstrutorDto>(instrutor); 
        }

        //POST
        public Result<ReadInstrutorDto> CriarInstrutor(CreateInstrutorDto createInstrutorDto)
        {
            Instrutor instrutorCadastrado = _context
                .Instrutores
                .FirstOrDefault(instrutor => instrutor.Email.ToUpper() == createInstrutorDto.Email.ToUpper());

            if(instrutorCadastrado == null)
            {
                Instrutor instrutor = _mapper.Map<Instrutor>(createInstrutorDto);

                _context.Add(instrutor);    
                _context.SaveChanges();
                ReadInstrutorDto readInstrutorDto = _mapper.Map<ReadInstrutorDto>(instrutor);
                return Result.Ok(readInstrutorDto).ToResult(i => i);
            }
            return Result.Fail($"Email já cadastrado!");
        }

        //PUT
        public Result AtualizarInstrutor(int id, UpdateInstrutorDto updateInstrutorDto)
        {
            Instrutor instrutor = _context.Instrutores
                .FirstOrDefault(instrutor => instrutor.Id == id);

            if (instrutor == null) return Result.Fail("Instrutor não encontrado");
            _mapper.Map(updateInstrutorDto, instrutor); //Atualizando o instrutor;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Instrutor atualizado com sucesso!");
        }

        //DELETE
        public Result ApagarInstrutor(int id)
        {
            Instrutor instrutor = _context.Instrutores
                .FirstOrDefault(instrutor => instrutor.Id == id);

            if (instrutor == null) return Result.Fail("Instrutor não encontrado!");
            instrutor.DeleteAt = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Instrutor apagado com sucesso!");
        }

        //PUT restaurar
        public Result RestaurarInstrutorPorID(int id)
        {
            Instrutor instrutor = _context.Instrutores
                .FirstOrDefault(instrutor => instrutor.Id == id);

            if (instrutor == null) return Result.Fail("Instrutor não encontrado!");
            instrutor.DeleteAt = null;
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Instrutor restaurado com sucesso!");
        }
    }
}
