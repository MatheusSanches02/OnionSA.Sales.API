﻿using Microsoft.AspNetCore.Http;
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


        [HttpPost("incluir")]
        public async Task<IActionResult> InserirArquivo(IFormFile criarArquivo)
        {
            try
            {
                if (criarArquivo == null || criarArquivo.Length == 0)
                {
                    return BadRequest("Objeto criarArquivo é nulo.");
                }
                var resultado = await _arquivosRepository.Inserir(criarArquivo);
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar arquivo!");
            }
        }
    }
}