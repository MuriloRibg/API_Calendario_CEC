using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Models
{
    public class Local
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Capacidade é obrigatório!")]
        public int Capacidade { get; set; }

        [Required(ErrorMessage = "O campo Sistemas é obrigatório!")]
        public bool Sistemas { get; set; }

        public DateTime? DeleteAt { get; set; }

        [JsonIgnore]
        public virtual List<Reserva> Reservas { get; set; }
    }
}
