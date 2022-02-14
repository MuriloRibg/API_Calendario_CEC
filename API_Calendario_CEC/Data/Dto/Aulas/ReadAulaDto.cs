using API_Calendario_CEC.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Aulas
{
    public class ReadAulaDto
    {
        public int Id { get; set; }
        public virtual Instrutor Instrutor { get; set; }
        public virtual Turma Turma { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public virtual Reserva Reserva { get; set; }
    }
}
