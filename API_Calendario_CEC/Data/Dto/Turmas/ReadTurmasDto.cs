using System;
using System.ComponentModel.DataAnnotations;
using API_Calendario_CEC.Models;

namespace API_Calendario_CEC.Data.Dto.Turmas
{
    public class ReadTurmasDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Quant_alunos é obrigatório!")]
        public int Quant_alunos { get; set; }

        [Required(ErrorMessage = "O campo Id_pilar é obrigatório!")]        

        public virtual Pilar Pilar { get; set; }
    }
}
