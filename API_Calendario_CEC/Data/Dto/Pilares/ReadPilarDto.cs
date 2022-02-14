using API_Calendario_CEC.Models;
using System.Collections.Generic;

namespace API_Calendario_CEC.Data.Dto.Pilares
{
    public class ReadPilarDto
    {
       
        public int Id { get; set; }
        public string NomePilar { get; set; }
        public string Categoria { get; set; }
        public string Cor { get; set; }
        public virtual List<Turma> Turmas { get; set; }
        public virtual List<Instrutor> Instrutor { get; set; }
    }
}
