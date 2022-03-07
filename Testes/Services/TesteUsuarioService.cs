using AutoMapper;
using Cinema.Api.Profiles;
using Data.Entities;
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
        public async void BuscaTodosUsuarios_UsuariosAtivos_RetornaTrue()
        {
            //arrange
            var usuarios = new List<Usuario>()
            {
                new Usuario(){IdUsuario=1, NomeUsuario="usuario", CargoUsuario=0, Situacao=SituacaoEntities.Arquivado},
                new Usuario(){IdUsuario=2, NomeUsuario="usuario2", CargoUsuario=0, Situacao=SituacaoEntities.Ativado},
            };
            _usuarioRepository.Setup(u=>u.BuscaTodos()).ReturnsAsync(usuarios);
            var usuariosDto = _mapper.Map<IEnumerable<LerUsuarioDto>>(usuarios);
            //act
            int skip = 1; int take = 3;
            var usuariosEncontradosAtivosServices = await _usuarioServices.BuscaTodosOsUsuarioDto(skip,take);

            bool usuariosAtivosDto()
            {
                foreach (var usuario in usuariosEncontradosAtivosServices)
                {
                    if (usuario.Situacao == SituacaoEntities.Ativado)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            //assert
            Assert.True(usuariosAtivosDto());
        }
        [Fact]
        public async void BuscaUsuarioPorId_UsuarioNaoEncontrado_RetornaNull()
        {
            //arrange
            _usuarioRepository.Setup(u => u.BuscarPorId(1)).ReturnsAsync(null as Usuario);
           

            //act
            var act = await _usuarioServices.BuscaUsuarioPorId(1);

            //assert
            Assert.Null(act);
        }
        [Fact]
        public async void BuscaUsuarioPorId_UsuarioArquivado_RetornaNull()
        {
            //arrange 
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario", Situacao = SituacaoEntities.Arquivado };
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            //act
            var act = await _usuarioServices.BuscaUsuarioPorId(usuario.IdUsuario);
            //assert
            Assert.Null(act);
        }
        [Fact]
        public async void BuscaUsuarioPorId_UsuarioAtivo_RetornaUsuario()
        {
            //arrange
            //arrange 
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario", Situacao = SituacaoEntities.Ativado};
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            var usuarioDto = _mapper.Map<LerUsuarioDto>(usuario);
            //act
            var act = await _usuarioServices.BuscaUsuarioPorId(usuarioDto.IdUsuario);
            //assert
            Assert.Equal(usuario.NomeUsuario, act.UserName);
            
        }

        //CRIAR USUARIO
        [Fact]
        public async void CriaUsuario_UsuarioCriadoOK_RetornaTrue()
        {
            //arrange
            var usuarioDto=new CriarUsuarioDto() { NomeUsuarioDto="usuario", EmailDto="aaa", Password="123", RePassword="123"};
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            _usuarioRepository.Setup(u => u.Cadastra(usuario)).Returns(Task.FromResult(usuario));
            //act
            var act = await _usuarioServices.CriarUsuarioNormalDto(usuarioDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);


        }


        //ALTERA USUARIO
        [Fact]
        public async void AlteraUsuario_UsuarioNaoEncontrado_RetornaNull()
        {
            //arrange
            _usuarioRepository.Setup(u => u.BuscarPorId(1)).ReturnsAsync(null as Usuario);
            var usuarioDto = new CriarUsuarioDto() { NomeUsuarioDto = "usuario" };
            //act
            var act = await _usuarioServices.AlteraUsuario(1, usuarioDto);
            //assert
            Assert.Null(act);

        }
        [Fact]
        public async void AlteraUsuario_UsuarioAlteradoOK_RetornaTrue()
        {
            //arrange
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "usuario", Situacao = SituacaoEntities.Ativado }; 
            var usuarioDto=_mapper.Map<CriarUsuarioDto>(usuario);
            _usuarioRepository.Setup(u => u.BuscarPorId(usuario.IdUsuario)).ReturnsAsync(usuario);
            //act
            var act= await _usuarioServices.AlteraUsuario(1,usuarioDto);
            var resultado=TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);

            //assert
            Assert.True(resultado);
        }


        

    }
}
