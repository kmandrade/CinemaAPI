using AutoMapper;
using Domain.Dtos.DiretorDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class DiretorProfile : Profile
    {
        public DiretorProfile()
        {
            CreateMap<Diretor, LerDiretorDto>().ReverseMap();
            CreateMap<CriarDiretorDto, Diretor>();
            CreateMap<AlterarDiretorDto, Diretor>();
        }

    }
}
