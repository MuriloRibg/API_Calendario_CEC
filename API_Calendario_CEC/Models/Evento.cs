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

        [Required]
        public int Id_Instrutor { get; set; }

        public virtual Instrutor Instrutor { get; set; }

        [JsonIgnore]
        public virtual Reserva Reserva { get; set; }

        public int Id_Reserva { get; set; }
    }
}
