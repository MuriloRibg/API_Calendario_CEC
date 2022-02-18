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
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo DataInicio é obrigatório!")]
        [DataType(DataType.Time)]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "O campo DataFim é obrigatório!")]
        [DataType(DataType.Time)]
        public string HoraFim { get; set; }

        [Required(ErrorMessage = "O campo Id_Local é obrigatório!")]
        public int Id_Local { get; set; }
        public virtual Local Local { get; set; }

        [JsonIgnore]
        public virtual Aula Aula { get; set; }

        [JsonIgnore]
        public virtual Evento Evento { get; set; }
    }
    
}
