using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Eventos
{
    public class CreateEventoDto
    {
        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }

        [Required(ErrorMessage = "O campo Id_Reserva é obrigatório!")]
        public int Id_Reserva { get; set; }
    }
}
