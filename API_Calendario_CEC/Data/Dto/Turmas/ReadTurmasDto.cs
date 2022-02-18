using API_Calendario_CEC.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto
{
    public class ReadTurmasDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Quant_alunos { get; set; }

        public Pilar Pilar { get; set; }
    }
}

