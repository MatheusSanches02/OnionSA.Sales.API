using OnioSA.Sales.API.Entities;
using Vendas.API.Entities;

namespace OnioSA.Sales.API.Repository.Pedidos
{
    public interface IPedidosRepository
    {
        public Task<ResultadoGenerico> Incluir(IFormFile pedido);
        public Task<ResultadoGenerico> ObterPedidos();
        public Task<ResultadoGenerico> VinculaRegiao(List<Pedido> pedidos);
        public abstract  Task<CepInfo> ConsultarViaCep(string cep);
    }
}
