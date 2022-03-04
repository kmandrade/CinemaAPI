using AutoMapper;
using Domain.Dtos.AtorDto;
using Domain.Dtos.AtorFilme;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class AtorFilmeProfile : Profile
    {
        public AtorFilmeProfile()
        {
            CreateMap<AtoresFilme, LerAtorFilmeDto>()
                .ReverseMap();
            CreateMap<CriarAtorFilmeDto, AtoresFilme>().ReverseMap();
            CreateMap<AtoresFilme, LerAtorDto>()
                .ForMember(dto => dto.NomeAtor, opt => opt.MapFrom(n => n.Ator.NomeAtor))
                .ReverseMap();
        }
    }
}
