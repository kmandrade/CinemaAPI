using AutoMapper;
using Domain.Dtos.VotosDto;
using Domain.Models;

namespace Cinema.Api.Profiles
{
    public class VotosProfile:Profile
    {
        public VotosProfile()
        {
            CreateMap<Votos,LerVotoDto>()
                .ForMember(dto=>dto.LerFilmeDto, opt=>opt.MapFrom(f=>f.Filme))
                .ForMember(dto=>dto.ValorDoVoto, opt=>opt.MapFrom(v=>v.ValorDoVoto))
                .ReverseMap();
            CreateMap<AdicionaVotosDto, Votos>()
                .ForMember(dto => dto.ValorDoVoto, opt => opt.MapFrom(v => v.ValorDoVotoDto))
                .ForMember(dto => dto.IdFilme, opt => opt.MapFrom(f => f.IdFilmeDto))
                .ForMember(dto => dto.IdUsuario, opt => opt.MapFrom(u => u.IdUsuarioDto));
                
        }

    }
}
