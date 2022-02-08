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
                .ForMember(dto=>dto.AtoresDto , opt=>opt.MapFrom(f=>f.AtoresFilme))
                .ForMember(dto=>dto.GenerosDto, opt=>opt.MapFrom(f=>f.GenerosFilme))
                .ReverseMap();
            CreateMap<CriarFilmeDto, Filme>();
            CreateMap<AlterarFilmeDto, Filme>();
            
        }
    }
}
