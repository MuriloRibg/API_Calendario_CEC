using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Models
{
    public class Instrutor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MinLength(5, ErrorMessage = "O campo Nome deve possuir no mínimo de 5 letras!")]
        [MaxLength(100, ErrorMessage ="O campo Nome deve possuir no máximo de 100 letras!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Abreviação é obrigatório!")]
        public string Abreviacao { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório!")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Disponibilidade é obrigatório!")]
        public string Disponibilidade { get; set; }

        [Required(ErrorMessage = "O campo Disponibilidade é obrigatório!")]
        public string Pilar { get; set; }
        
        public DateTime? DeleteAt { get; set; }

        [JsonIgnore]
        public virtual List<Evento> Eventos { get; set; }

        [JsonIgnore]
        public virtual List<Aula> Aulas { get; set; }
    }
}
