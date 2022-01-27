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
            CreateMap<Filme, LerFilmeDto>().ReverseMap();
            CreateMap<CriarFilmeDto, Filme>();
            CreateMap<AlterarFilmeDto, Filme>();

            


        }
    }
}
