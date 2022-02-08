using Domain.Dtos.GeneroDto;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Entities;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneroController : ControllerBase
    {
        IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet("ConsultaGeneros")]
        public IActionResult ConsultaGeneros()
        {
            var generos = _generoService.ConsultaTodos();
            return Ok(generos);
        }

        [HttpPost]
        public IActionResult CadastraGenero([FromBody]CriarGeneroDto generoDto)
        {
            _generoService.Cadastra(generoDto);
            return Ok();
        }
        
        [HttpDelete("DeletaGeneros/{id}")]
        public IActionResult DeletaGenero(int id)
        {
            _generoService.Excluir(id);
            return Ok();
        }
    }
}
