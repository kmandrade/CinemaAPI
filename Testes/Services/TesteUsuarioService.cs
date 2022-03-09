using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Domain.Dtos.UsuarioDto;
using Domain.Models;
using Moq;
using Servicos.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.BaseEntities;
using Xunit;

namespace Testes.Services
{
    public class TesteUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UsuarioServices _usuarioServices;
        private readonly Mock<IUsuarioRepository> _usuarioRepository;

        public TesteUsuarioService()
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsuarioProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _usuarioRepository = new Mock<IUsuarioRepository>();

            _usuarioServices = new UsuarioServices(_usuarioRepository.Object, _mapper);
        }


        //BUSCA
        [Fact]
        public async Task BuscarTodosUsuarios_UsuariosAtivos_RetornaTrue()
        {
            //arrange
            var usuarios = new List<Usuario>()
            {
                new Usuario(){IdUsuario=1, NomeUsuario="usuario", CargoUsuario=0, Situacao=SituacaoEntities.Arquivado},
                new Usuario(){IdUsuario=2, NomeUsuario="usuario2", CargoUsuario=0, Situacao=SituacaoEntities.Ativado},
            };
            _usuarioRepository.Setup(u => u.BuscarTodos()).ReturnsAsync(usuarios);
            var usuariosDto = _mapper.Map<IEnumerable<LerUsuarioDto>>(usuarios);
            //act
            int skip = 1; int take = 3;
            var usuariosEncontradosAtivosServices = await _usuarioServices.BuscarTodosOsUsuarioDto(skip, take);
            bool resultado = true;
            
                foreach (var usuario in usuariosEncontradosAtivosServices)
                {
                    if (usuario.Situacao == SituacaoEntities.Arquivado)
                    {
                        resultado=false;
                    }
          
                }
                 
            
            //assert
            Assert.True(resultado);
        }
        [Fact]
        public async Task BuscarUsuarioPorId_UsuarioNaoEncontrado_RetornaNull()
        {
            //arrange
            _usuarioRepository.Setup(u => u.BuscarPorId(1)).ReturnsAsync(null as Usuario);


            //act
            var act = await _usuarioServices.BuscarUsuarioPorId(1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Usuario(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task BuscarUsuarioPorId_UsuarioArquivado_RetornaFalse()
        {
            //arrange 
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario", Situacao = SituacaoEntities.Arquivado };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            //act
            var act = await _usuarioServices.BuscarUsuarioPorId(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Usuario(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task BuscarUsuarioPorId_UsuarioAtivo_RetornaTrue()
        {
            //arrange
            //arrange 
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario", Situacao = SituacaoEntities.Ativado };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            var usuarioDto = _mapper.Map<LerUsuarioDto>(usuario);
            //act
            var act = await _usuarioServices.BuscarUsuarioPorId(usuarioDto.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Usuario(act);
            //assert
            Assert.True(resultado);

        }

        //CRIAR USUARIO
        [Fact]
        public async Task CriarUsuario_UsuarioCriadoOK_RetornaTrue()
        {
            //arrange
            var usuarioDto = new CriarUsuarioDto() { NomeUsuarioDto = "usuario", EmailDto = "aaa", Password = "123", RePassword = "123" };
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            _usuarioRepository.Setup(u => u.Cadastrar(usuario)).Returns(Task.FromResult(usuario));
            //act
            var act = await _usuarioServices.CriarUsuarioNormalDto(usuarioDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);


        }


        //Alterar USUARIO
        [Fact]
        public async Task AlterarUsuario_UsuarioNaoEncontrado_RetornaNull()
        {
            //arrange
            _usuarioRepository.Setup(u => u.BuscarPorId(1)).ReturnsAsync(null as Usuario);
            var usuarioDto = new CriarUsuarioDto() { NomeUsuarioDto = "usuario" };
            //act
            var act = await _usuarioServices.AlterarUsuario(1, usuarioDto);
            //assert
            Assert.Null(act);

        }
        [Fact]
        public async Task AlterarUsuario_UsuarioAlterardoOK_RetornaTrue()
        {
            //arrange
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario", Situacao = SituacaoEntities.Ativado };
            var usuarioDto = _mapper.Map<CriarUsuarioDto>(usuario);
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            //act
            var act = await _usuarioServices.AlterarUsuario(1, usuarioDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);

            //assert
            Assert.True(resultado);
        }


        //Arquivar Usuario
        [Fact]
        public async Task ArquivarUsuario_UsuarioNaoExiste_RetornaFalse()
        {
            //arrange
            _usuarioRepository.Setup(u => u.BuscarPorId(1)).ReturnsAsync(null as Usuario);

            //act
            var act = await _usuarioServices.ArquivarUsuario(1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);

            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task ArquivarUsuario_UsuarioJaArquivado_RetornaFalse()
        {
            //arrange
            var usuario = new Usuario()
            { IdUsuario = 1, NomeUsuario = "usuario",
                Situacao = SituacaoEntities.Arquivado
            };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario));
            //act
            var act = await _usuarioServices.ArquivarUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ArquivarUsuario_UsuarioAtivo_RetornaTrue()
        {
            //arrange
            var usuario = new Usuario()
            {
                IdUsuario = 1,
                NomeUsuario = "usuario",
                Situacao = SituacaoEntities.Ativado
            };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);

            //act
            var act = await _usuarioServices.ArquivarUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);


            //assert
            Assert.True(resultado);

        }


        //Reativar Usuario

        [Fact]
        public async Task ReativarUsuario_UsuarioNaoExiste_RetornaFalse()
        {
            //arrange
            _usuarioRepository.Setup(u => u.BuscarPorId(1)).ReturnsAsync(null as Usuario);

            //act
            var act = await _usuarioServices.ReativarUsuario(1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);

            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task ReativarUsuario_UsuarioJaAtivado_RetornaFalse()
        {
            //arrange
            var usuario = new Usuario()
            {
                IdUsuario = 1,
                NomeUsuario = "usuario",
                Situacao = SituacaoEntities.Ativado
            };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario));
            //act
            var act = await _usuarioServices.ReativarUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ReativarUsuario_UsuarioAtviado_RetornaTrue()
        {
            //arrange
            var usuario = new Usuario()
            {
                IdUsuario = 1,
                NomeUsuario = "usuario",
                Situacao = SituacaoEntities.Arquivado
            };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);

            //act
            var act = await _usuarioServices.ReativarUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);


            //assert
            Assert.True(resultado);

        }


        //DELETAR USUARIO
        [Fact]
        public async Task ExcluirUsuario_UsuarioNaoExiste_RetornaFalse()
        {
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario" };
            _usuarioRepository.Setup(u => u.Excluir(usuario));

            //
            var act = await _usuarioServices.ExcluirUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //
            Assert.False(resultado);

        }
        [Fact]
        public async Task ExcluirUsuario_UsuarioAtivo_RetornaFalse()
        {
            var usuario = new Usuario()
            {
                IdUsuario = 1,
                NomeUsuario = "usuario",
                Situacao = SituacaoEntities.Ativado
            };
            _usuarioRepository.Setup(u => u.Excluir(usuario));

            //
            var act = await _usuarioServices.ExcluirUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //
            Assert.False(resultado);


        }
        [Fact]
        public async Task ExcluirUsuario_UsuarioArquivado_RetornaTrue()
        {
            var usuario = new Usuario()
            {
                IdUsuario = 1,
                NomeUsuario = "usuario",
                Situacao = SituacaoEntities.Arquivado
            };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            _usuarioRepository.Setup(u => u.Excluir(usuario));

            //
            var act = await _usuarioServices.ExcluirUsuario(usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //
            Assert.True(resultado);


        }

    }
}
