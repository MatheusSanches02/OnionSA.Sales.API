using System;
using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnioSA.Sales.API.Entities;
using OnioSA.Sales.API.Repository.Arquivos;
using OnioSA.Sales.API.Repository.Pedidos;

namespace OnioSA.Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IArquivosRepository _arquivosRepository;

        public PedidosController(IPedidosRepository pedidosRepository, IArquivosRepository arquivosRepository)
        {
            _pedidosRepository = pedidosRepository;
            _arquivosRepository = arquivosRepository;
        }
        [HttpPost("IncluirPedido")]
        public async Task<IActionResult> IncluirPedido(IFormFile pedido)
        {
            try
            {
                if (pedido == null || pedido.Length == 0)
                {
                    return BadRequest("Arquivo Excel não fornecido ou vazio.");
                }

                await _arquivosRepository.Inserir(pedido);

                var resultado = await _pedidosRepository.Incluir(pedido);
               

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar arquivo!");
            }
        }
    }
}
