using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Data.Dto.Eventos
{
    public class CreateEventoDto
    {
        public CreateEventoDto(int id_Instrutor, string desc, int id_Reserva)
        {
            Id_Instrutor = id_Instrutor;
            Id_Reserva = id_Reserva;
            Descricao = desc;
        }

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }

        [Required(ErrorMessage = "O campo Id_Reserva é obrigatório!")]
        public int Id_Reserva { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        public string Descricao { get; set; }
    }
}
