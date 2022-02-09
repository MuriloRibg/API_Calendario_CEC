using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Models
{
    public class Reserva
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatório!")]
        [MaxLength(100, ErrorMessage = "O campo Titulo deve possuir no máximo de 100 letras!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo DataInicio é obrigatório!")]
        [StringLength(20, ErrorMessage = "O campo DataInicio deve possuir 20 caracteres")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo DataFim é obrigatório!")]
        [StringLength(20, ErrorMessage = "O campo DataInicio deve possuir 20 caracteres")]
        public DateTime DataFim { get; set; }
       
        public virtual Local Local { get; set; }

        public int Id_Local { get; set; }

        [JsonIgnore]
        public virtual List<Aula> Aulas { get; set; }

        [JsonIgnore]
        public virtual List<Evento> Eventos { get; set; }
    }
    
}
