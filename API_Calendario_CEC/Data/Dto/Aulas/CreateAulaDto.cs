using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Aulas
{
    public class CreateAulaDto
    {
        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }


        [Required(ErrorMessage = "O campo Id_Turma é obrigatório!")]
        public int Id_Turma { get; set; }

        [Required(ErrorMessage = "O campo Id_Disciplina é obrigatório!")]
        public int Id_Disciplina { get; set; }
        public int Id_Reserva { get; set; }
    }
}
