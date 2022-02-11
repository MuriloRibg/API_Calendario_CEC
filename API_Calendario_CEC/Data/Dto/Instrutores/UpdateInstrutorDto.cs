using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto
{
    public class UpdateInstrutorDto
    {

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [MinLength(5, ErrorMessage = "O campo nome deve possuir no mínimo de 5 letras!")]
        [MaxLength(100, ErrorMessage = "O campo nome deve possuir no máximo de 100 letras!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Abreviação é obrigatório!")]
        public string Abreviacao { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        public string Email { get; set; }

        public string Disponibilidade { get; set; }

        [Required(ErrorMessage = "O campo Disponibilidade é obrigatório!")]
        public int Id_Pilar { get; set; }
    }
}
