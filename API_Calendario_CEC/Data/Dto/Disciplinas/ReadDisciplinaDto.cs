using API_Calendario_CEC.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Calendario_CEC.Data.Dto.Disciplinas
{
    public class ReadDisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual Pilar Pilar { get; set; }
        [JsonIgnore]
        public virtual List<Aula> Aulas { get; set; }
    }
}
