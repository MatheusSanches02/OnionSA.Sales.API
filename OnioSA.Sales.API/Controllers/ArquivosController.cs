using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnioSA.Sales.API.Entities;
using OnioSA.Sales.API.Repository.Arquivos;

namespace OnioSA.Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivosController : ControllerBase
    {
        private readonly IArquivosRepository _arquivosRepository;

        public ArquivosController(IArquivosRepository arquivosRepository)
        {
            _arquivosRepository = arquivosRepository;
        }


        [HttpPost("Incluir")]
        public async Task<IActionResult> InserirArquivo(IFormFile pedido)
        {
            try
            {
                if (pedido == null || pedido.Length == 0)
                {
                    return BadRequest("Objeto criarArquivo é nulo.");
                }
                var resultado = await _arquivosRepository.Inserir(pedido);
                return Ok(resultado);
                 
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar arquivo!");
            }
        }

        [HttpGet("ObterLista")]
        public async Task<IActionResult> ObterLista()
        {
            try
            {
                var resultado = await _arquivosRepository.ObterArquivos();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter arquivos!");
            }
        }
    }
}
