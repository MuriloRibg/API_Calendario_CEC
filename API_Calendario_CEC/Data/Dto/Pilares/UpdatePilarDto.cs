using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Pilares
{
    public class UpdatePilarDto
    {
        [Required(ErrorMessage = "O campo NomePilar é obrigatório!")]
        public string NomePilar { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório!")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O campo Cor é obrigatório!")]
        public string Cor { get; set; }
    }
}
