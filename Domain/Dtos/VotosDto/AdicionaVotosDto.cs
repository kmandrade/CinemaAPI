using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.VotosDto
{
    public class AdicionaVotosDto
    {
        public int IdFilmeDto { get; set; }
        [Range(0,4,ErrorMessage =("O valor so pode ser de 0 a 4"))]
        public int  ValorDoVotoDto { get; set; }
        
    }
}
