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

        

        public IEnumerable<LerUsuarioDto> BuscaTodosOsUsuarioDto(int skip, int take)
        {
            var listaUsuarios = _usuarioDao.BuscarTodos().Skip(skip).Take(take).ToList();
            var usuariosDto = _mapper.Map<IEnumerable<LerUsuarioDto>>(listaUsuarios);
            return usuariosDto;
        }

        public LerUsuarioDto BuscaUsuarioPorId(int idUsuario)
        {
            var usuarioSelecionado = _usuarioDao.BuscarPorId(idUsuario);
            var usuarioDto = _mapper.Map<LerUsuarioDto>(usuarioSelecionado);
            return usuarioDto;
        }


        public void CriarUsuarioNormalDto(CriarUsuarioDto criarUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(criarUsuarioDto);
            usuario.Situacao = SituacaoEntities.Ativado;
            _usuarioDao.Incluir(usuario);
        }

        public void DeletaUsuario(int idUsuario)
        {
            var usuarioSelecionado = _usuarioDao.BuscarPorId(idUsuario);
            _usuarioDao.Excluir(usuarioSelecionado);
        }

        public void AlteraUsuario(int idUsuario, CriarUsuarioDto criarUsuarioDto)
        {
            var usuarioSelecionado = _usuarioDao.BuscarPorId(idUsuario);
            _mapper.Map(usuarioSelecionado, criarUsuarioDto);
            _usuarioDao.Save();
        }

        public Result ArquivarUsuario(int id)
        {
            var usuarioSelecionado = _usuarioDao.BuscarPorId(id);
            if(usuarioSelecionado==null || usuarioSelecionado.Situacao == SituacaoEntities.Arquivado)
            {
                return Result.Fail("Usuario nao existe ou ja arquivado");
            }
            usuarioSelecionado.Situacao = SituacaoEntities.Arquivado;
            _usuarioDao.Alterar(usuarioSelecionado);
            return Result.Ok();
        }
        public Result ReativarUsuario(int id)
        {
            var usuarioSelecionado = _usuarioDao.BuscarPorId(id);
            if (usuarioSelecionado == null || usuarioSelecionado.Situacao == SituacaoEntities.Ativado)
            {
                return Result.Fail("Usuario nao existe ou ja ativado");
            }
            usuarioSelecionado.Situacao = SituacaoEntities.Ativado;
            _usuarioDao.Alterar(usuarioSelecionado);
            return Result.Ok();
        }

        public IEnumerable<LerUsuarioDto> BuscaUsuariosArquivados(int skip, int take)
        {
            var ususarios = _usuarioDao.BuscarTodos()
                .Where(u => u.Situacao == SituacaoEntities.Arquivado).Skip(skip).Take(take)
                .ToList();
            var ususariosMapeados = _mapper.Map<IEnumerable<LerUsuarioDto>>(ususarios);
            return ususariosMapeados;
        }

       public Usuario BuscaUsuarioPorLogin(LoginRequest loginRequest)
       {
           var usuarioSelecionado =
               _usuarioDao.BuscaUsuarioPorNomeESenha(loginRequest.UserName, loginRequest.Password);
           return usuarioSelecionado;
       }
       


    }
}
