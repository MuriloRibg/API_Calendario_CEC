using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Models
{
    public class Evento 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }

        [Required(ErrorMessage = "O campo Id_Reserva é obrigatório!")]
        public int Id_Reserva { get; set; }

        public virtual Reserva Reserva { get; set; }

        public virtual Instrutor Instrutor { get; set; }
    }
}
