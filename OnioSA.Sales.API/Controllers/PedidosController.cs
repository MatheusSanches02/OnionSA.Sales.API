using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

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
                using (var package = new ExcelPackage(excelFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Supondo que os dados estejam na primeira planilha.

                    

                }

                return Ok("Dados do Excel processados e salvos no banco de dados com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao processar o arquivo Excel: {ex.Message}");
            }
        }
    }
}
