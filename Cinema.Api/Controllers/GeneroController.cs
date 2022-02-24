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
        public async Task<IActionResult> ConsultaGeneros([FromQuery] int skip, int take)
        {
            var generos = await _generoService.ConsultaTodos(skip,take);
            return Ok(generos);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CadastraGenero([FromBody]CriarGeneroDto generoDto)
        {
           await _generoService.Cadastra(generoDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraNomeGenero/{id}")]
        public async Task<IActionResult> AlteraNomeGenero(int id, AlterarGeneroDto obj)
        {
            await _generoService.Altera(id, obj);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaGenero/{id}")]
        public async Task<IActionResult> DeletaGenero(int id)
        {
            await _generoService.Excluir(id);
            return Ok();
        }
    }
}
