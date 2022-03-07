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
            CreateMap<Usuario, LerUsuarioDto>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(u => u.NomeUsuario))
                .ReverseMap();
            CreateMap<CriarUsuarioDto, Usuario>()
                .ForMember(dto => dto.NomeUsuario, opt => opt.MapFrom(u => u.NomeUsuarioDto))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(u => u.EmailDto))
                .ForMember(dto => dto.Password, opt => opt.MapFrom(u => u.Password)).ReverseMap();
                
            CreateMap<Usuario, LoginRequest>()
                .ForMember(dto=>dto.UserName ,opt=>opt.MapFrom(u=>u.NomeUsuario))
                .ForMember(dto=>dto.Password ,opt=>opt.MapFrom(u=>u.Password))
                .ReverseMap();
        }
    }
}
