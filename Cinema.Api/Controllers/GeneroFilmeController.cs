using Domain.Dtos.FilmeGenero;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Entities;

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
        [HttpDelete("DeletaGeneroDoFilme/{id}")]
        public IActionResult DeletaGeneroDoFilme(int id)
        {
            _generoFilmeService.DeletaGeneroDoFilme(id);
            return Ok();
        }
        
        
    }
}
