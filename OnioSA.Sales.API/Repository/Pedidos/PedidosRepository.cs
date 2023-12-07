using Aspose.Cells;
using Newtonsoft.Json;
using OnioSA.Sales.API.Entities;
using OnioSA.Sales.API.Persistence;
using Vendas.API.Entities;

namespace OnioSA.Sales.API.Repository.Pedidos
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly SalesDbContext _context;
        public PedidosRepository(SalesDbContext context)
        {
            _context = context;
        }

        public Task<ResultadoGenerico> Incluir(IFormFile pedido)
        {
            try
            {
                using (var stream = pedido.OpenReadStream())
                {
                    Workbook wb = new Workbook(stream);

                    WorksheetCollection collection = wb.Worksheets;

                    for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
                    {
                        Worksheet worksheet = collection[worksheetIndex];

                        Console.WriteLine("Worksheet: " + worksheet.Name);

                        int rows = worksheet.Cells.MaxDataRow;
                        int cols = worksheet.Cells.MaxDataColumn;

                        for (int i = 0; i < rows + 1; i++)
                        {
                            var tempList = new List<object>();
                            if (i >= 1)
                            {
                                for (int j = 0; j < cols + 1 ; j++)
                                {
                                
                                    tempList.Add(worksheet.Cells[i, j].Value);

                                }

                                var produtoNome = tempList[3]?.ToString();

                                var produto = _context.Produtos.FirstOrDefault(p => p.Nome == produtoNome);

                                if (produto == null)
                                {
                                    return Task.FromResult(new ResultadoGenerico(false, "A palnilha possui um produto inválido"));
                                }
                                var pedidoBd = new Pedido()
                                {
                                    Documento = tempList[0]?.ToString(),
                                    RazaoSocial = tempList[1]?.ToString(),
                                    CEP = tempList[2]?.ToString(),
                                    Produto = tempList[3]?.ToString(),
                                    NumPedido = int.TryParse(tempList[4]?.ToString(), out int numPedido) ? numPedido : 0,
                                    Data = DateTime.TryParse(tempList[5]?.ToString(), out DateTime dataPedido) ? dataPedido : DateTime.MinValue,
                                    ValorProduto = produto.Valor
                                };
                                _context.Pedidos.Add(pedidoBd);
                                _context.SaveChanges();
                            }
                            
                        }
                    }
                }

                return Task.FromResult(new ResultadoGenerico(true, "Arquivo criado com sucesso!"));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar arquivo!", ex);
            }
        }

        public async Task<ResultadoGenerico> ObterPedidos()
        {
            try
            {
                var listaPedidos =  _context.Pedidos.ToList();

                await VinculaRegiao(listaPedidos);
                
                return new ResultadoGenerico(true, "Sucesso ao obter lista de arquivos!", listaPedidos);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter arquivo!", ex);
            }
        }

        public async Task<ResultadoGenerico> VinculaRegiao(List<Pedido> listaPedidos)
        {
            try
            {

                foreach (var pedido in listaPedidos)
                {
                    if (!string.IsNullOrWhiteSpace(pedido.CEP))
                    {
                        var cepInfo = await ConsultarViaCep(pedido.CEP);


                        if (cepInfo != null)
                        {
                            switch (cepInfo.UF)
                            {
                                case "SP":
                                    pedido.Regiao = cepInfo.UF;
                                    pedido.Frete = 0; 
                                    pedido.Prazo = 0;
                                    break;
                                case "MG":
                                case "RJ":
                                case "ES":
                                    pedido.Regiao = "SUDESTE";
                                    pedido.Frete = pedido.ValorProduto * 0.1m;
                                    pedido.Prazo = 1;
                                    break;
                                case "RO":
                                case "AC":
                                case "AM":
                                case "RR":
                                case "PA":
                                case "AP":
                                case "TO":
                                case "MA":
                                case "PI":
                                case "CE":
                                case "RN":
                                case "PB":
                                case "PE":
                                case "AL":
                                case "SE":
                                case "BA":
                                    pedido.Regiao = "NORTE/NORDESTE";
                                    pedido.Frete = pedido.ValorProduto * 0.3m; 
                                    pedido.Prazo = 10;
                                    break;
                                case "PR":
                                case "SC":
                                case "RS":
                                case "MS":
                                case "MT":
                                case "GO":
                                case "DF":
                                    pedido.Regiao = "CENTRO/SUL";
                                    pedido.Frete = pedido.ValorProduto * 0.2m; 
                                    pedido.Prazo = 5;
                                    break;

                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();

                return new ResultadoGenerico(true, "Sucesso ao vincular região aos pedidos!", listaPedidos);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao vincular região aos pedidos!", ex);
            }
        }
        public async Task<CepInfo> ConsultarViaCep(string cep)
        {
            using (var httpClient = new HttpClient())
            {
                var viaCepUrl = $"https://viacep.com.br/ws/{cep}/json/";

                var response = await httpClient.GetStringAsync(viaCepUrl);

                var cepInfo = JsonConvert.DeserializeObject<CepInfo>(response);

                return cepInfo;
            }
        }
    }
}
