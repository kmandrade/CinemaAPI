using Domain.Dtos.FilmeGenero;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
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

        [HttpPost("AdicionaGeneroEmFilme")]
        public IActionResult AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            _generoFilmeService.AdicionaGeneroFilme(criarGeneroFilmeDto);
            return Ok();
        }
        [HttpDelete("DeletaGeneroDoFilme")]
        public IActionResult DeletaGeneroDoFilme([FromQuery] int idGenero,int idFilme)
        {
            _generoFilmeService.DeletaGeneroDoFilme(idGenero,idFilme);
            return Ok();
        }
        
        
    }
}
