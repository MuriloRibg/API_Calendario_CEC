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

        [Required(ErrorMessage = "O campo Id_Pilar é obrigatório!")]
        public int Id_Pilar { get; set; }

        public virtual Pilar Pilar { get; set; }

        [JsonIgnore]
        public virtual List<Aula> Aulas { get; set; }
    }
}
