using Domain.Dtos.GeneroDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;


namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeneroController : ControllerBase
    {
         IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet("ConsultaTodosGeneros")]
        public IActionResult ConsultaGeneros()
        {
            var generos = _generoService.ConsultaTodos();
            return Ok(generos);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastraGenero([FromBody]CriarGeneroDto generoDto)
        {
            _generoService.Cadastra(generoDto);
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraNomeGenero/{id}")]
        public IActionResult AlteraNomeGenero(int id, AlterarGeneroDto obj)
        {
            _generoService.Altera(id, obj);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaGenero/{id}")]
        public IActionResult DeletaGenero(int id)
        {
            _generoService.Excluir(id);
            return Ok();
        }
    }
}
