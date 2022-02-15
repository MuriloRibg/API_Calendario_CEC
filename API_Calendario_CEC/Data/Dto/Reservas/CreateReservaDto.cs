using API_Calendario_CEC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Data.Dto.Reservas
{
    public class CreateReservaDto
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo DataInicio é obrigatório!")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo DataFim é obrigatório!")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O campo Id_Local é obrigatório!")]
        public int Id_Local { get; set; }
    }
}
