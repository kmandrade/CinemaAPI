using AutoMapper;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<Filme, LerFilmeDto>()
                .ForMember(dto => dto.AtoresDto, opt => opt.MapFrom(f => f.AtoresFilme))
                .ForMember(dto => dto.GenerosDto, opt => opt.MapFrom(f => f.GenerosFilme))
                .ForMember(dto => dto.DiretorDto, opt => opt.MapFrom(f => f.Diretor))
                .ForMember(dto=>dto.Votos, opt=>opt.MapFrom(v=>v.Votos))
                .ReverseMap();
            CreateMap<CriarFilmeDto, Filme>().ReverseMap();
            CreateMap<AlterarFilmeDto, Filme>().ReverseMap()
                .ForMember(dto => dto.Titulo, opt => opt.MapFrom(f => f.Titulo))
                .ForMember(dto => dto.Duracao, opt => opt.MapFrom(f => f.Duracao));
                
                
        }
    }
}
