using API_Calendario_CEC.Models;

namespace API_Calendario_CEC.Data.Dto.Eventos
{
    public class ReadEventoDto
    {
        public int Id { get; set; }
        public int Id_Instrutor { get; set; }
        public int Id_Reserva { get; set; }
        public virtual Reserva Reserva { get; set; }
        public virtual Instrutor Instrutor { get; set; }
    }
}
