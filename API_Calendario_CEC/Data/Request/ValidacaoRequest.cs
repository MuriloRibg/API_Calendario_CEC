namespace API_Calendario_CEC.Data.Request
{
    public class ValidacaoRequest
    {
        public bool Validacao { get; set; }
        public string Message { get; set; }

        public ValidacaoRequest(bool validacao, string message)
        {
            Message = message;
            Validacao = validacao;
        }
        
    }
}
