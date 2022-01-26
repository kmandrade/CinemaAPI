using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class RelacionamentoNN
    {
        public int AtorId { get; set; }
        public int GeneroId { get; set; }

        public int VotosId { get; set; }

        public Ator Ator { get; set; }
        public Genero Genero { get; set; }
        public Votos Votos { get; set; }
    }
}
