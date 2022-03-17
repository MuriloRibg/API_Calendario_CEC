using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Aulas
{

    public class UpdateAulaDto
    {

        [Required(ErrorMessage = "O campo Id é obrigatório!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }
        [Required(ErrorMessage = "O campo Id_Turma é obrigatório!")]
        public int Id_Turma { get; set; }
        [Required(ErrorMessage = "O campo Id_Disciplina é obrigatório!")]
        public int Id_Disciplina { get; set; }

        public string? Descricao { get; set; }

        public UpdateAulaDto(int id, int id_Instrutor, int id_Turma, int id_Disciplina, string descricao)
        {
            Id = id;
            Id_Instrutor = id_Instrutor;
            Id_Turma = id_Turma;
            Id_Disciplina = id_Disciplina;
            Descricao = descricao;
        }

    }
}
