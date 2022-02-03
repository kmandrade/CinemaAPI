using AutoMapper;
using Domain.Dtos.FilmeGenero;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class GeneroFilmeProfile : Profile
    {
        public GeneroFilmeProfile()
        {
            CreateMap<GeneroFilme, LerGeneroFilmeDto>().ReverseMap();
            CreateMap<CriarGeneroFilmeDto, GeneroFilme>();
        }

    }
}
