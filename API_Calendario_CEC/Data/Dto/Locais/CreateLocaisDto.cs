using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Locais {
    public class CreateLocaisDto {
        
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [MinLength(3, ErrorMessage = "O campo Nome deve possuir no mínimo de 5 letras!")]
        [MaxLength(50, ErrorMessage = "O campo nome deve possuir no máximo de 50 letras!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Capacidade é obrigatório!")]
        public int Capacidade { get; set; }

        [Required(ErrorMessage = "O campo Sistemas é obrigatório!")]
        public bool Sistemas { get; set; }
    }
}