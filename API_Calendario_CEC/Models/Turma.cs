using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Models
{
    public class Turma
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Quant_alunos é obrigatório!")]
        public int Quant_alunos { get; set; }

        [Required(ErrorMessage = "O campo Id_pilar é obrigatório!")]
        public int Id_Pilar { get; set; }

        public virtual Pilar Pilar { get; set; }
    }
}
