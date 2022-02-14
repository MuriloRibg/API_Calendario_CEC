using API_Calendario_CEC.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto
{
    public class ReadInstrutorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Abreviacao { get; set; }
        
        public string Email { get; set; }
        
        public string Disponibilidade { get; set; }
        public string Pilar { get; set; }
    }
}
