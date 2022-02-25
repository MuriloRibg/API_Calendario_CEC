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

        public FullCalendarRequest(string title, string start, string end, string color)
        {
            this.title = title;
            this.start = start;
            this.end = end;
            this.color = color;
        }
    }
}
