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

        [Required(ErrorMessage = "O campo DataInicio é obrigatório!")]
        [DataType(DataType.Time)]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "O campo DataFim é obrigatório!")]
        [DataType(DataType.Time)]
        public string HoraFim { get; set; }

        [Required(ErrorMessage = "O campo Id_Local é obrigatório!")]
        public int Id_Local { get; set; }

        [Required(ErrorMessage = "O campo TipoEvento é obrigatório!")]
        public string TipoEvento { get; set; }

        //campos para informar se aula ou evento que serão tratados no service de reservas
        public int Id_Instrutor { get; set; }
        public int Id_Turma { get; set; }
        public int Id_Disciplina { get; set; }

        //campo caso seja evento
        public string Descricao { get; set; }
    }
}
