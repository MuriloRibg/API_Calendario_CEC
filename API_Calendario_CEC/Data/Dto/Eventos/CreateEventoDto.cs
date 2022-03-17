using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Eventos
{
    public class CreateEventoDto
    {
        public CreateEventoDto(int id_Instrutor, string desc, int id_Reserva)
        {
            Id_Instrutor = id_Instrutor;
            Descricao = desc;
            Id_Reserva = id_Reserva;
        }

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }

        [Required(ErrorMessage = "O campo Id_Reserva é obrigatório!")]
        public int Id_Reserva { get; set; }
        public string Descricao { get; set; }
    }
}
