using Domain.Dtos.AtorDto;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AtorController : ControllerBase
    {
        private readonly IAtorService _atorService;

        public AtorController(IAtorService atorService)
        {
            _atorService = atorService;
        }

        [HttpGet("ConsultaAtores")]
        public async Task<IActionResult> BuscaAtores([FromQuery] int skip, int take)
        {
            if (skip <= 0 || take <= 0) return BadRequest(new { message = "Paginacao Errada" });
            var atores = await _atorService.BuscarTodos(skip, take);
            if (atores == null)
            {
                return BadRequest(new { message = "Error ao buscar Atores" });
            }

            return Ok(atores);
        }

        [HttpGet("ConsultaAtorPorId/{id}")]
        public async Task<IActionResult> BuscaAtorPorId(int id)
        {
            if (id <= 0)
            {

                return BadRequest(new { message = "O id precisa ser maior que 0" });

            }
            var ator = await _atorService.BuscarPorId(id);
            if (ator.IsFailed)
            {
                return BadRequest(new { message = "Ator Nao encontrado" });
            }
            return Ok(ator.ValueOrDefault);

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CadastrarAtor([FromBody] CriarAtorDto atorDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Result resultado = await _atorService.Cadastrarr(atorDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("AlterarNomeAtor/{id}")]
        public async Task<IActionResult> AlterarNomeAtor(int id, AlterarAtorDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0) { return BadRequest(new { message = "Id precisa ser maior que 0" }); }
            var resultado = await _atorService.Alterar(id, obj);
            if (resultado.IsFailed) { return BadRequest(new { message = resultado.ToString() }); }
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtor/{id}")]
        public async Task<IActionResult> DeletaAtor(int id)
        {
            if (id <= 0) { return BadRequest(new { message = "Id precisa ser maior que 0" }); }
            var resultado = await _atorService.Excluir(id);
            if (resultado.IsFailed) { return BadRequest(new { message = resultado.ToString() }); }
            return Ok();
        }
    }
}
