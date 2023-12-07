using Aspose.Cells;
using OnioSA.Sales.API.Entities;
using OnioSA.Sales.API.Persistence;

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
                                var pedidoBd = new Pedido()
                                {
                                    Documento = tempList[0]?.ToString(),
                                    RazaoSocial = tempList[1]?.ToString(),
                                    CEP = tempList[2]?.ToString(),
                                    Produto = tempList[3]?.ToString(),
                                    NumPedido = int.TryParse(tempList[4]?.ToString(), out int numPedido) ? numPedido : 0,
                                    Data = DateTime.TryParse(tempList[5]?.ToString(), out DateTime dataPedido) ? dataPedido : DateTime.MinValue
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
    }
}
