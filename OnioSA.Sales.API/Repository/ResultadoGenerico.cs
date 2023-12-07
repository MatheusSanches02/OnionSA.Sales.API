namespace OnioSA.Sales.API.Repository
{
    public class ResultadoGenerico
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public object? Dados { get; set; }

        public ResultadoGenerico(bool sucesso, string? mensagem, object? dados = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}
