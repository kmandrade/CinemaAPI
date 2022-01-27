using AutoMapper;
using Domain.Dtos.GeneroDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class GeneroProfile : Profile
    {
        public GeneroProfile()
        {
            CreateMap<Genero, LerGeneroDto>().ReverseMap();
            CreateMap<CriarGeneroDto, Genero>();
            CreateMap<AlterarGeneroDto, Genero>();
        }
    }
}
