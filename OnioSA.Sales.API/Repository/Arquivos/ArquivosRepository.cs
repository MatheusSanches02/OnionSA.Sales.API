using OnioSA.Sales.API.Entities;
using OnioSA.Sales.API.Persistence;
using System;

namespace OnioSA.Sales.API.Repository.Arquivos
{
    public class ArquivosRepository : IArquivosRepository
    {
        private readonly SalesDbContext _context;
        public ArquivosRepository(SalesDbContext context)
        {
            _context = context;
        }
        public Task<ResultadoGenerico> Inserir(IFormFile arquivo) 
        {
            try
            {
                using (var arquivoEmMemoria = new MemoryStream())
                {
                    arquivo.CopyTo(arquivoEmMemoria);

                    var arquivoCriado = new Arquivo()
                    {
                        Arq = arquivoEmMemoria.ToArray(),
                        Extensao = arquivo.FileName.Split('.')[^1],
                        NomeArquivo = arquivo.FileName,
                        TipoArquivo = arquivo.ContentType,
                        TamanhoArquivo = arquivoEmMemoria.ToArray().Length
                    };

                    _context.Arquivo.Add(arquivoCriado);
                    _context.SaveChanges();
                    return Task.FromResult(new ResultadoGenerico(true, "Arquivo criado com sucesso!"));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar arquivo!", ex);
            }
        }

        public Task<ResultadoGenerico> ObterArquivos() 
        {
            try
            {
                return Task.FromResult(new ResultadoGenerico(true, "Sucesso ao obter lista de arquivos!"));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter arquivo!", ex);
            }
        }

    }
}
