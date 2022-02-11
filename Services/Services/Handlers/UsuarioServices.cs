using AutoMapper;
using Data.Entities;
using Domain.Dtos.UsuarioDto;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
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

        public void CriarUsuarioDto(CriarUsuarioDto criarUsuarioDto)
        {
            
        }

        public IEnumerable<LerUsuarioDto> LerTodosOsUsuarioDto()
        {
            var listaUsuarios= _usuarioDao.BuscarTodos();
            var usuariosDto=_mapper.Map<IEnumerable<LerUsuarioDto>>(listaUsuarios);
            return usuariosDto;
        }
    }
}
