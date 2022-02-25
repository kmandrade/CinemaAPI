using AutoMapper;
using Data.Entities;
using Domain.Dtos.UsuarioDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Handlers
{
    public class UsuarioServices : IUsuarioService
    {

        IUsuarioDao _usuarioDao;
        IMapper _mapper;
        public UsuarioServices(IUsuarioDao usuarioDao, IMapper mapper)
        {
            _usuarioDao = usuarioDao;
            _mapper = mapper;
        }



        public async Task<IEnumerable<LerUsuarioDto>> BuscaTodosOsUsuarioDto(int skip, int take)
        {
            var listaUsuarios = await _usuarioDao.BuscaTodos();
            var usuariosPaginados = listaUsuarios.Skip(skip).Take(take).ToList();
            var usuariosDto = _mapper.Map<IEnumerable<LerUsuarioDto>>(listaUsuarios);
            return usuariosDto;
        }

        public async Task<LerUsuarioDto> BuscaUsuarioPorId(int idUsuario)
        {
            var usuarioSelecionado = await _usuarioDao.BuscarPorId(idUsuario);
            var usuarioDto = _mapper.Map<LerUsuarioDto>(usuarioSelecionado);
            return usuarioDto;
        }


        public async Task<Result> CriarUsuarioNormalDto(CriarUsuarioDto criarUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(criarUsuarioDto);
            usuario.Situacao = SituacaoEntities.Ativado;
            await _usuarioDao.Incluir(usuario);
            return Result.Ok();
        }

        public async Task<Result> DeletaUsuario(int idUsuario)
        {
            var usuarioSelecionado = await _usuarioDao.BuscarPorId(idUsuario);
            _usuarioDao.Excluir(usuarioSelecionado);
            return Result.Ok();
        }

        public async Task<Result> AlteraUsuario(int idUsuario, CriarUsuarioDto criarUsuarioDto)
        {
            var usuarioSelecionado = await _usuarioDao.BuscarPorId(idUsuario);
            _mapper.Map(usuarioSelecionado, criarUsuarioDto);
            await _usuarioDao.Save();
            return Result.Ok();
        }

        public async Task<Result> ArquivarUsuario(int id)
        {
            var usuarioSelecionado = await _usuarioDao.BuscarPorId(id);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Arquivado)
            {
                return Result.Fail("Usuario nao existe ou ja arquivado");
            }
            usuarioSelecionado.Situacao = SituacaoEntities.Arquivado;
            await _usuarioDao.Alterar(usuarioSelecionado);
            return Result.Ok();
        }
        public async Task<Result> ReativarUsuario(int id)
        {
            var usuarioSelecionado = await _usuarioDao.BuscarPorId(id);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Ativado)
            {
                return Result.Fail("Usuario nao existe ou ja ativado");
            }
            usuarioSelecionado.Situacao = SituacaoEntities.Ativado;
            await _usuarioDao.Alterar(usuarioSelecionado);
            return Result.Ok();
        }

        public async Task<IEnumerable<LerUsuarioDto>> BuscaUsuariosArquivados(int skip, int take)
        {
            var ususarios = await _usuarioDao.BuscaTodos();
            var usuariosPaginados=ususarios
                .Where(u => u.Situacao == SituacaoEntities.Arquivado).Skip(skip).Take(take)
                .ToList();
            var ususariosMapeados = _mapper.Map<IEnumerable<LerUsuarioDto>>(ususarios);
            return ususariosMapeados;
        }

        public async Task<Usuario> BuscaUsuarioPorLogin(LoginRequest loginRequest)
        {
            var usuarioSelecionado = await
                _usuarioDao.BuscaUsuarioPorNomeESenha(loginRequest.UserName, loginRequest.Password);
            return usuarioSelecionado;
        }



    }
}
