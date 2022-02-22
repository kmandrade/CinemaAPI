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
        public  IActionResult BuscaFilmesPorAtor([FromQuery] int idAtor)
        {
            var filmes = _atorFilmeService.BuscaFilmesPorAtor(idAtor);
            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaAtorEmFilme")]
        public IActionResult AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            
            Result resultado = _atorFilmeService.AdicionaAtorFilme(criarAtorFilmeDto);
            if(resultado.IsFailed)
            {
                return BadRequest();
            }
            return Ok();
            
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraAtorDofilme")]
        public IActionResult AlteraAtorDofilme([FromQuery] int idAtorAtual, int idFilme, int idAtorNovo)
        {
            Result resultado =  _atorFilmeService.AlteraAtorDoFilme(idAtorAtual, idFilme, idAtorNovo);
            if (resultado.IsFailed)
            {
                return BadRequest();
            }
            return Ok();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtorDoFilme")]
        public IActionResult DeletaAtorDoFilme([FromQuery] int idAtor,int idFilme)
        {
            Result resultado = _atorFilmeService.DeletaAtorDoFilme(idAtor,idFilme);
            if (resultado != null)
            {
                return Ok();
            }
            return BadRequest();
        }
        

    }
}
