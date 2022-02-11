using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.VotosDto
{
    public class AdicionaVotosDto
    {
        public int IdFilmeDto { get; set; }
        public int  ValorDoVotoDto { get; set; }
        public int IdUsuarioDto { get; set; }
    }
}
