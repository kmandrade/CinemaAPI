using AutoMapper;
using Domain.Dtos.AtorDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class AtorProfile:Profile
    {
        public AtorProfile()
        {
            CreateMap<Ator, LerAtorDto>().ReverseMap();
            CreateMap<CriarAtorDto, Ator>();
            CreateMap<AlterarAtorDto, Ator>();
        }
        
    }
}
