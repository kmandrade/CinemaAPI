using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;

namespace Domain.Dtos.AtorFilme
{
    public class LerAtorFilmeDto
    {
        public int IdAtorFilmeDto { get; set; }
        public LerAtorDto Ator { get; set; }
        public LerFilmeDto Filme { get; set; }
    }
}
