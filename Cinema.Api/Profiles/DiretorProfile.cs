using AutoMapper;
using Domain.Dtos.DiretorDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class DiretorProfile : Profile
    {
        public DiretorProfile()
        {
            CreateMap<Diretor, LerDiretorDto>()
                .ForMember(dto => dto.NomeDiretor, opt => opt.MapFrom(d => d.NomeDiretor))
                .ReverseMap();
            CreateMap<CriarDiretorDto, Diretor>().ReverseMap();
            CreateMap<AlterarDiretorDto, Diretor>().ReverseMap();
        }

    }
}
