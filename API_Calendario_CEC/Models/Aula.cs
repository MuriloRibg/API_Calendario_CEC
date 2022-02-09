using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Models
{
    public class Aula
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Id_Instrutor { get; set; }

        public virtual Instrutor Instrutor { get; set; }

        [Required]
        public int Id_Turma { get; set; }

        public virtual Turma Turma { get; set; }

        [Required]
        public int Id_Disciplina { get; set; }

        public virtual Disciplina Disciplina { get; set; }

        [Required]
        public int Id_Reserva { get; set; }

        public virtual Reserva Reserva { get; set; }
    }
}
