using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.UsuarioDto;
using Domain.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Servicos.Services.InterfacesService;
using Utils.Cripitografia;

namespace Servicos.Services.Handlers
{
    public class UsuarioServices : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IHash _hash;
        public UsuarioServices(IUsuarioRepository usuarioRepository, IMapper mapper, IHash hash)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _hash = hash;
        }

        public async Task<Usuario> BuscarUsuarioPorLogin(string nome)
        {
            var usuarioSelecionado = await
                _usuarioRepository.BuscarUsuarioPorLogin(nome);
            return usuarioSelecionado;
        }

        public async Task<IEnumerable<LerUsuarioDto>> BuscarTodosOsUsuarioDto(int skip, int take)
        {

            var listaUsuarios = await _usuarioRepository.BuscarTodos();

            var usuariosPaginados = listaUsuarios.Where(u => u.Situacao == SituacaoEntities.Ativado).ToList();

            foreach (var usuarios in usuariosPaginados)
            {
                if (usuarios.Situacao == SituacaoEntities.Arquivado)
                {
                    return null;
                }
            }
            var usuariosDto = _mapper.Map<IEnumerable<LerUsuarioDto>>(listaUsuarios);

            return usuariosDto.Skip(skip).Take(take).OrderBy(u => u.UserName);
        }

        public async Task<Result<LerUsuarioDto>> BuscarUsuarioPorId(int idUsuario)
        {
            var usuarioSelecionado = await _usuarioRepository.BuscarPorId(idUsuario);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Arquivado)
            { return Result.Fail("Usuario nao encontrado"); }
            var usuarioDto = _mapper.Map<LerUsuarioDto>(usuarioSelecionado);
            return Result.Ok(usuarioDto);
        }


        public async Task<Result> CriarUsuarioHash(CriarUsuarioDto criarUsuario)

        {
            var usuario = _mapper.Map<Usuario>(criarUsuario);
            var usuarioConsultado = await _usuarioRepository.BuscarUsuarioPorLogin(criarUsuario.NomeUsuarioDto);
            if (usuarioConsultado != null)
            {
                return Result.Fail("Ja possui usuario com este nome");
            }
            _hash.TransformaEmHash(usuario);
            usuario.Situacao = SituacaoEntities.Ativado;
            await _usuarioRepository.Cadastrar(usuario);
            return Result.Ok();

        }


        public async Task<Result> AlterarUsuario(int idUsuario, CriarUsuarioDto criarUsuarioDto)
        {

            var usuarioConsultado = _usuarioRepository.BuscarPorId(idUsuario);
            if (usuarioConsultado == null)
            {
                return Result.Fail("Nao possui usuario com esse id");
            }
            var usuario = _mapper.Map<Usuario>(criarUsuarioDto);

            _hash.TransformaEmHash(usuario);
            await _usuarioRepository.Alterar(usuario);
            return Result.Ok();
            
        }
        

        public async Task<Result> ArquivarUsuario(int id)
        {
            var usuarioSelecionado = await _usuarioRepository.BuscarPorId(id);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Arquivado)
            {
                return Result.Fail("Usuario nao existe ou ja arquivado");
            }
            usuarioSelecionado.Situacao = SituacaoEntities.Arquivado;
            await _usuarioRepository.Alterar(usuarioSelecionado);
            return Result.Ok();
        }
        public async Task<Result> ReativarUsuario(int id)
        {
            var usuarioSelecionado = await _usuarioRepository.BuscarPorId(id);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Ativado)
            {
                return Result.Fail("Usuario nao existe ou ja ativado");
            }
            usuarioSelecionado.Situacao = SituacaoEntities.Ativado;
            await _usuarioRepository.Alterar(usuarioSelecionado);
            return Result.Ok();
        }

        public async Task<IEnumerable<LerUsuarioDto>> BuscarUsuariosArquivados(int skip, int take)
        {
            var ususarios = await _usuarioRepository.BuscarTodos();
            var usuariosPaginados = ususarios
                .Where(u => u.Situacao == SituacaoEntities.Arquivado).Skip(skip).Take(take)
                .ToList();
            var ususariosMapeados = _mapper.Map<IEnumerable<LerUsuarioDto>>(usuariosPaginados);
            return ususariosMapeados;
        }



        public async Task<Result> ExcluirUsuario(int idUsuario)
        {

            var usuarioSelecionado = await _usuarioRepository.BuscarPorId(idUsuario);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Ativado)
            {
                return Result.Fail("usuario ativo ou nao encontrado");
            }
            await _usuarioRepository.Excluir(usuarioSelecionado);
            return Result.Ok();
        }


        

        public async Task<bool> ValidaSenhaAsync(LoginRequest login)
        {
            
            var usuario = _mapper.Map<Usuario>(login);
            var usuarioConsultado = await _usuarioRepository.BuscarUsuarioPorLogin(usuario.NomeUsuario);
            if (usuarioConsultado == null)
            {
                return false;
            }

            if (usuarioConsultado.NomeUsuario == "admin")
            {
                ConverteSenhaEmHash(usuarioConsultado);
            }

           return await _hash.ValidaSenha(usuario, usuarioConsultado.Password);

        }
        private void ConverteSenhaEmHash(Usuario usuario)
        {

            var passwordHasher = new PasswordHasher<Usuario>();
            usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);
        }

    }
}
