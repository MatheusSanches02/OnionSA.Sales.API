namespace OnioSA.Sales.API.Entities
{
    public class Arquivo
    {
        public Guid CodArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Arq { get; set; }
        public string Extensao { get; set; }
        public string TipoArquivo { get; set; }
        public int TamanhoArquivo { get; set; }
    }
}
