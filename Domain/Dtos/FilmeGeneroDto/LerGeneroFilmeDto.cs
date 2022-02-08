using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.FilmeGenero
{
    public class LerGeneroFilmeDto
    {
        public int Id { get; set; }
        public LerFilmeDto FilmeDto { get; set; }
        public LerGeneroDto GeneroDto { get; set; }
    }
}
