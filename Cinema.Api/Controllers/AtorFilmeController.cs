using Domain.Dtos.AtorFilme;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;
using Servicos.Services.Handlers;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AtorFilmeController : ControllerBase

    {
        private IAtorFilmeService _atorFilmeService;

        public AtorFilmeController(IAtorFilmeService atorFilmeService)
        {
            _atorFilmeService = atorFilmeService;
        }
        [HttpGet("BuscaFilmesPorAtor")]
        public  async Task<IActionResult> BuscaFilmesPorAtor([FromQuery] int idAtor)
        {
            var filmes = await _atorFilmeService.BuscaFilmesPorAtor(idAtor);
            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaAtorEmFilme")]
        public async Task<IActionResult> AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            
            Result resultado = await _atorFilmeService.AdicionaAtorFilme(criarAtorFilmeDto);
            if(resultado.IsFailed)
            {
                return BadRequest();
            }
            return Ok();
            
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraAtorDofilme")]
        public async Task<IActionResult> AlteraAtorDofilme([FromQuery] int idAtorAtual, int idFilme, int idAtorNovo)
        {
            Result resultado = await  _atorFilmeService.AlteraAtorDoFilme(idAtorAtual, idFilme, idAtorNovo);
            if (resultado.IsFailed)
            {
                return BadRequest();
            }
            return Ok();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtorDoFilme")]
        public async Task<IActionResult> DeletaAtorDoFilme([FromQuery] int idAtor,int idFilme)
        {
            Result resultado = await _atorFilmeService.DeletaAtorDoFilme(idAtor,idFilme);
            if (resultado != null)
            {
                return Ok();
            }
            return BadRequest();
        }
        

    }
}
