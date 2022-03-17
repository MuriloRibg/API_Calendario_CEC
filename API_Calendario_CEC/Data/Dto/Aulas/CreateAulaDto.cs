using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Aulas
{
    public class CreateAulaDto
    {
        public CreateAulaDto(int id_Instrutor, int id_Turma, int id_Disciplina, string descricao, int id_Reserva)
        {
            Id_Instrutor = id_Instrutor;
            Id_Turma = id_Turma;
            Id_Disciplina = id_Disciplina;
            Descricao = descricao;
            Id_Reserva = id_Reserva;
        }

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }


        [Required(ErrorMessage = "O campo Id_Turma é obrigatório!")]
        public int Id_Turma { get; set; }

        [Required(ErrorMessage = "O campo Id_Disciplina é obrigatório!")]
        public int Id_Disciplina { get; set; }

        public int Id_Reserva { get; set; }

        public string Descricao { get; set; }
    }
}
