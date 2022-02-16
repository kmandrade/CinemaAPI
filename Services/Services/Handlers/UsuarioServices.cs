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

        public Usuario BuscaUsuarioPorLogin(LoginRequest loginRequest)
        {
            var usuarioSelecionado =
                _usuarioDao.BuscaUsuarioPorNomeESenha(loginRequest.UserName, loginRequest.Password);
            return usuarioSelecionado;
        }

        public void CriarUsuarioNormalDto(CriarUsuarioDto criarUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(criarUsuarioDto);
            _usuarioDao.Incluir(usuario);
        }

        public IEnumerable<LerUsuarioDto> LerTodosOsUsuarioDto()
        {
            var listaUsuarios= _usuarioDao.BuscarTodos();
            var usuariosDto=_mapper.Map<IEnumerable<LerUsuarioDto>>(listaUsuarios);
            return usuariosDto;
        }
    }
}
