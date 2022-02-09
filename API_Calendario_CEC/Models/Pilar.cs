using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Models
{
    public class Pilar
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo NomePilar é obrigatório!")]
        public string NomePilar { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório!")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O campo Cor é obrigatório!")]
        public string Cor { get; set; }

        [JsonIgnore]
        public virtual List<Turma> Turmas { get; set; }

        [JsonIgnore]
        public virtual List<Disciplina> Disciplinas { get; set; }

        [JsonIgnore]
        public virtual List<PilaresInstrutor> PilaresInstrutor { get; set; }
    }
}
