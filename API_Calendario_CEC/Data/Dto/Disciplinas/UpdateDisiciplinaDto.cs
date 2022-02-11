using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Disciplinas
{
    public class UpdateDisiciplinaDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Pilar é obrigatório!")]
        public string Pilar { get; set; }
    }
}
