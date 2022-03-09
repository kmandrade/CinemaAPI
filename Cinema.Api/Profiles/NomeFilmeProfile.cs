using AutoMapper;
using Domain.Dtos.FilmeDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class NomeFilmeProfile : Profile
    {
        public NomeFilmeProfile()
        {
            CreateMap<Filme, LerNomeFilmeDto>()
                .ForMember(dto=>dto.NomeFilme, opt=>opt.MapFrom(f=>f.Titulo)).ReverseMap();
        }
    }
}
