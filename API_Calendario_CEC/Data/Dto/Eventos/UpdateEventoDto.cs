using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Eventos
{
    public class UpdateEventoDto
    {

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }

        [Required(ErrorMessage = "O campo Id_Reserva é obrigatório!")]
        public int Id { get; set; }

        public UpdateEventoDto(int id_Instrutor, int id_evento)
        {
            Id_Instrutor = id_Instrutor;
            Id = id_evento;
        }
    }
}
