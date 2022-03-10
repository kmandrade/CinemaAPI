using Domain.Dtos.GeneroDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;


namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet("ConsultarTodosGeneros")]
        public async Task<IActionResult> BuscarGeneros([FromQuery] int skip, int take)
        {
            if (skip <= 0 || take <= 0)
            {
                return BadRequest(new { message = "Paginacao Errada" });
            }
            var generos = await _generoService.BuscarTodos(skip, take);
            if (generos == null)
            {
                return BadRequest(new { message = "Nao foi possivel buscar os generos" });
            }
            return Ok(generos);
        }
        [HttpGet("BuscarGeneroPorId/{id}")]
        public async Task<IActionResult> BuscarGeneroPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "id precisa ser maior que 0"});
            }
            var generos = await _generoService.BuscarPorId(id);
            if (generos.IsFailed)
            {
                return BadRequest(generos.ToString());
            }
            return Ok(generos.ValueOrDefault);
        }



        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CadastrarGenero([FromBody] CriarGeneroDto generoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _generoService.Cadastrar(generoDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlterarNomeGenero/{id}")]
        public async Task<IActionResult> AlterarNomeGenero(int id, AlterarGeneroDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
            {
                return BadRequest(new { message = "id precisa ser maior que 0" });
            }
            var resultado = await _generoService.Alterar(id, obj);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaGenero/{id}")]
        public async Task<IActionResult> ExcluirGenero(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "id precisa ser maior que 0" });
            }
            var resultado = await _generoService.Excluir(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }
    }
}
