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

        [Required(ErrorMessage = "O campo nomePilar é obrigatório!")]
        public string NomePilar { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório!")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O campo cor é obrigatório!")]
        public string Cor { get; set; }

        [JsonIgnore]
        public virtual List<Turma> Turmas { get; set; }     
    }
}
