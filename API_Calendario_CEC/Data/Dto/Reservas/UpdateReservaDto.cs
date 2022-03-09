using API_Calendario_CEC.Data.Dto.Aulas;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Data.Dto.Reservas
{
    public class UpdateReservaDto
    {   
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

        [Required(ErrorMessage = "O campo TipoEvento é obrigatório!")]
        public string TipoEvento { get; set; }

        [Required(ErrorMessage = "O campo Id_Local é obrigatório!")]
        public int Id_Local { get; set; }

        public int Id_Aula { get; set; }
        
        public int Id_Instrutor { get; set; }
       
        public int Id_Turma { get; set; }
        
        public int Id_Disciplina { get; set; }
        

    }
    
}
