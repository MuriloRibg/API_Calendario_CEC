using System;
using API_Calendario_CEC.Models;

namespace API_Calendario_CEC.Data.Request
{
    public class FullCalendarRequest
    { 
        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string color { get; set; }

        public string Descricao { get; set; }

        public string Instrutor { get; set; }

        public string Turma { get; set; }

        public string Local { get; set; }

        public string Disciplina { get; set; }

        public FullCalendarRequest(
            string title,
            string start,
            string end,
            string color,
            string descricao,
            string instrutor,
            string local,
            string turma = null,
            string disciplina = null
            )
        {
            this.title = title;
            this.start = start;
            this.end = end;
            this.color = color;
            Descricao = descricao;
            Instrutor = instrutor;
            Turma = turma;
            Local = local;
            Disciplina = disciplina;
        }
    }
}
