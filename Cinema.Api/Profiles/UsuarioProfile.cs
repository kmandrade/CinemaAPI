using AutoMapper;
using Domain.Dtos.UsuarioDto;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Api.Profiles
{
    public class UsuarioProfile:Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario,LerUsuarioDto>().ReverseMap();
            CreateMap<CriarUsuarioDto,Usuario>();
            CreateMap<Usuario, LoginRequest>()
                .ForMember(dto=>dto.UserName ,opt=>opt.MapFrom(u=>u.NomeUsuario))
                .ForMember(dto=>dto.Password ,opt=>opt.MapFrom(u=>u.Password))
                .ReverseMap();
        }
    }
}
