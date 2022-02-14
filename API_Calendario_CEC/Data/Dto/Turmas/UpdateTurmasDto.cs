using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto
{
    public class UpdateTurmasDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Quant_alunos é obrigatório!")]
        public int Quant_alunos { get; set; }

        [Required(ErrorMessage = "O campo Pilar é obrigatório!")]
        public string Pilar { get; set; }
    }
}
