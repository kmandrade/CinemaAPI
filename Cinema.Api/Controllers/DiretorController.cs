using Domain.Dtos.DiretorDto;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DiretorController : ControllerBase
    {
        private readonly IDiretorService _diretorService;

        public DiretorController(IDiretorService diretorService)
        {
            _diretorService = diretorService;
        }
        [HttpGet("BuscaFilmesPorDiretor")]
        public async Task<IActionResult> BuscarFilmesPorDiretor([FromQuery] int iDdiretor)
        {
            if (iDdiretor <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var filmes = await _diretorService.lerFilmeDtosPorDiretor(iDdiretor);
            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound(new { message = "Filmes nao encontrado" });
        }
        [HttpGet("BuscarTodosDiretores")]
        public async Task<IActionResult> BuscarDiretores([FromQuery]int skip, int take)
        {
            if(skip <= 0 || take <= 0)
            {
                return BadRequest(new { message = "Paginacao Errada" });
            }
            var diretores = await _diretorService.ConsultarTodos(skip, take);
            if (diretores == null)
            {
                return BadRequest(new { message = "Diretores nao encontrados" });
            }
            return Ok(diretores);
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarDiretoresPorId(int idDiretor)
        {
            if (idDiretor <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var diretor = await _diretorService.ConsultarPorId(idDiretor);

            if (diretor != null)
            {
                return Ok(diretor);
            }
            return NotFound(new { message = "Diretor nao encontrado" });
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CadastrarDiretor([FromBody]CriarDiretorDto criarDiretorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado =  await _diretorService.Cadastrar(criarDiretorDto);
            if(resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarDiretor(int id, [FromBody] AlterarDiretorDto diretor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado= await _diretorService.Alterar(id, diretor);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirDiretor(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado = await _diretorService.Excluir(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();

        }
       

    }
}
