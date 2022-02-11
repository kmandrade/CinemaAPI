using Domain.Dtos.FilmeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.VotosDto
{
    public class LerVotoDto
    {
        public int ValorDoVoto { get; set; }

        public LerFilmeDto LerFilmeDto { get; set; }

        
    }
}
