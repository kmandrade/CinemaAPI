using AutoMapper;
using Domain.Dtos.AtorFilme;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class AtorFilmeProfile : Profile
    {
        public AtorFilmeProfile()
        {
            CreateMap<AtoresFilme, LerAtorFilmeDto>().ReverseMap();
            CreateMap<CriarAtorFilmeDto, AtoresFilme>();
            
        }
    }
}
