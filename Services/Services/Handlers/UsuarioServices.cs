using AutoMapper;
using Data.Entities;
using Domain.Dtos.UsuarioDto;
using Domain.Models;
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

        

        public IEnumerable<LerUsuarioDto> BuscaTodosOsUsuarioDto()
        {
            var listaUsuarios = _usuarioDao.BuscarTodos();
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




       
       public Usuario BuscaUsuarioPorLogin(LoginRequest loginRequest)
       {
           var usuarioSelecionado =
               _usuarioDao.BuscaUsuarioPorNomeESenha(loginRequest.UserName, loginRequest.Password);
           return usuarioSelecionado;
       }
       


    }
}
