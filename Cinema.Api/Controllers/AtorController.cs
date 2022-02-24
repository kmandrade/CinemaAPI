using Domain.Dtos.AtorDto;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AtorController : ControllerBase
    {
        IAtorService _atorService;

        public AtorController(IAtorService atorService)
        {
            _atorService = atorService;
        }
        
        [HttpGet("ConsultaAtores")]
        public async Task<IActionResult> ConsultaAtores([FromQuery] int skip, int take)
        {
            if (skip < 0 || take < 0) return BadRequest();
            var atores = await _atorService.ConsultaTodos(skip,  take);
            if (atores == null) return BadRequest();
            return Ok(atores);
        }

        [HttpGet("ConsultaAtorPorId/{id}")]
        public async Task<IActionResult> ConsultaAtorPorId(int id)
        {
            var atores = await _atorService.ConsultaPorId(id);
            if (id < 0 || atores==null)
            {
                return BadRequest();
            }
            return Ok(atores);

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CadastraAtor([FromBody]CriarAtorDto atorDto)
        {
           Result resultado =  await _atorService.Cadastra(atorDto);
            if (resultado.IsFailed)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraNomeAtor/{id}")]
        public async Task<IActionResult> AlteraNomeAtor(int id, AlterarAtorDto obj)
        {
            await _atorService.Altera(id, obj);
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtor/{id}")]
        public async Task<IActionResult> DeletaAtor(int id)
        {
            await _atorService.Excluir(id);
            return Ok();
        }
    }
}
