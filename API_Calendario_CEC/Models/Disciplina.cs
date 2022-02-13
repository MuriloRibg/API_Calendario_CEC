using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Models
{
    public class Disciplina
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Pilar é obrigatório!")]
        public string Pilar { get; set; }

        public DateTime? DeleteAt { get; set; }

        [JsonIgnore]
        public virtual List<Aula> Aulas { get; set; }
    }
}
