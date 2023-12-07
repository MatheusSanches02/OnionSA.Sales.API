namespace OnioSA.Sales.API.Repository.Pedidos
{
    public interface IPedidosRepository
    {
        public Task<ResultadoGenerico> Incluir(IFormFile pedido);
    }
}
