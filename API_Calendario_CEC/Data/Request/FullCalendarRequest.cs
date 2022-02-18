using System;
using API_Calendario_CEC.Models;

namespace API_Calendario_CEC.Data.Request
{
    public class FullCalendarRequest
    { 
        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public string color { get; set; }                
    }
}
