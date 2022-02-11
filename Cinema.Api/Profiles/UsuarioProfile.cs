using AutoMapper;
using Domain.Dtos.UsuarioDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class UsuarioProfile:Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario,LerUsuarioDto>().ReverseMap();
            CreateMap<CriarUsuarioDto,Usuario>();
        }
    }
}
