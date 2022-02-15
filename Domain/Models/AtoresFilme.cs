using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AtoresFilme
    {
        [Key]
        public int IdAtoresFilme { get; set; }

        public virtual Filme Filme { get; set; }
        public int IdFilme { get; set; }

        public virtual Ator Ator { get; set; }
        public int IdAtor { get; set; }
    }
}
