using System;
namespace API_Calendario_CEC.Helps
{
    public class ValidacaoHelp
    {
        public static bool ValidarHorario(string horaIncio, string horaFim)
        {
            string[] horaInicioSplit = horaIncio.Split(":");
            int horaIncioFormatada = Int32.Parse($"{horaInicioSplit[0]}{horaInicioSplit[1]}");

            string[] horaFimSplit = horaFim.Split(":");
            int horaFimFormatada = Int32.Parse($"{horaFimSplit[0]}{horaFimSplit[1]}");

            if(horaIncioFormatada > horaFimFormatada)
            {
                return true;
            }
            return false;            
        }
    }
}
