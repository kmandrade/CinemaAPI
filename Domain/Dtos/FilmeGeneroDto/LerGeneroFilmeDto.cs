using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;

namespace Domain.Dtos.FilmeGenero
{
    public class LerGeneroFilmeDto
    {
        public int Id { get; set; }
        public LerFilmeDto FilmeDto { get; set; }
        public LerGeneroDto GeneroDto { get; set; }
    }
}
