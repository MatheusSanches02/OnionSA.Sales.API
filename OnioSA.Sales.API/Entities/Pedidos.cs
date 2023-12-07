using System.ComponentModel.DataAnnotations;

namespace OnioSA.Sales.API.Entities
{
    public class Pedido
    {
        [Key]
        public Guid CodPedido { get; set; }
        public string Documento { get; set; }
        public string RazaoSocial { get; set; }
        public string CEP { get; set; }
        public string Produto { get; set; }
        public int NumPedido { get; set; }
        public DateTime Data { get; set; }
        public int? Prazo { get; set; }
        public decimal? Frete { get; set; }
        public string? Regiao { get; set; }
        public decimal? ValorProduto { get; set; }
    }
}
