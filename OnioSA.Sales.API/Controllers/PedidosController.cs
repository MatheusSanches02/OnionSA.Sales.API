using System;
using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnioSA.Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        [HttpPost("upload")]
        public IActionResult UploadFile(IFormFile excelFile)
        {
            try
            {
                if (excelFile == null || excelFile.Length == 0)
                {
                    return BadRequest("Arquivo Excel não fornecido ou vazio.");
                }


                using (var stream = excelFile.OpenReadStream())
                {
                    Workbook wb = new Workbook(stream);

                    WorksheetCollection collection = wb.Worksheets; 

                    for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
                    {
                        Worksheet worksheet = collection[worksheetIndex];

                        Console.WriteLine("Worksheet: " + worksheet.Name);

                        int rows = worksheet.Cells.MaxDataRow;
                        int cols = worksheet.Cells.MaxDataColumn;

                        for (int i = 0; i < rows; i++)
                        {

                            for (int j = 0; j < cols; j++)
                            {
                                Console.Write(worksheet.Cells[i, j].Value + " | ");
                            }
                            Console.WriteLine(" ");
                        }
                    }
                }

                return Ok("Dados do Excel processados e salvos no banco de dados com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao processar o arquivo Excel. Detalhes: {ex.Message}");
            }
        }
    }
}
