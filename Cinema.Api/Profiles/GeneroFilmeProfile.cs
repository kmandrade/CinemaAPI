using AutoMapper;
using Domain.Dtos.FilmeGenero;
using Domain.Dtos.GeneroDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class GeneroFilmeProfile : Profile
    {
        public GeneroFilmeProfile()
        {
            CreateMap<GeneroFilme, LerGeneroFilmeDto>()
                .ForMember(dto => dto.FilmeDto, opt => opt.MapFrom(f => f.Filme))
                .ForMember(dto => dto.GeneroDto, opt => opt.MapFrom(g => g.Genero))
                .ReverseMap();
            CreateMap<CriarGeneroFilmeDto, GeneroFilme>();
            CreateMap<GeneroFilme, LerGeneroDto>()
                .ForMember(dto => dto.NomeGenero,
                opt => opt.MapFrom(n => n.Genero.NomeGenero))
                .ReverseMap();


                
        }

    }
}
