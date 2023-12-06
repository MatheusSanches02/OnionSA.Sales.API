using OnioSA.Sales.API.Entities;

namespace OnioSA.Sales.API.Repository.Arquivos
{
    public interface IArquivosRepository
    {
        public Task<ResultadoGenerico> Inserir(IFormFile arquivo);
        public Task<ResultadoGenerico> ObterArquivos();
    }
}
