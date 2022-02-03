using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AtoresFilme
    {
        public int Id { get; set; }

        public Filme Filme { get; set; }
        public int IdFilme { get; set; }

        public Ator Ator { get; set; }
        public int IdAtor { get; set; }
    }
}
