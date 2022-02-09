using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AtorFilme
{
    public class LerAtorFilmeDto
    {
        public int IdAtorFilmeDto { get; set; }
        public LerAtorDto Ator { get; set; }
        public LerFilmeDto Filme { get; set; }
    }
}
