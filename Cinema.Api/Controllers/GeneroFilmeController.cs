using Domain.Dtos.FilmeGenero;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeneroFilmeController : ControllerBase
    {
        private IGeneroFilmeService _generoFilmeService;

        public GeneroFilmeController(IGeneroFilmeService generoFilmeService)
        {
            _generoFilmeService = generoFilmeService;
        }


        [HttpGet("BuscaFilmesPorGenero")]
        public IActionResult BuscarFilmesPorGenero([FromQuery] int iDGenero)
        {
            var gf = _generoFilmeService.BuscarFilmesPorGenero(iDGenero);
            return Ok(gf);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaGeneroEmFilme")]
        public IActionResult AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            _generoFilmeService.AdicionaGeneroFilme(criarGeneroFilmeDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("AlteraGeneroDoFilme")]
        IActionResult AlteraGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo)
        {
            _generoFilmeService.AlteraGeneroDoFilme(idGeneroAntigo, idFilme, iDGeneroNovo);
            return Ok ();
        }


        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaGeneroDoFilme")]
        public IActionResult DeletaGeneroDoFilme([FromQuery] int idGenero,int idFilme)
        {
            _generoFilmeService.DeletaGeneroDoFilme(idGenero,idFilme);
            return Ok();
        }
        
        
    }
}
