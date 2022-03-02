using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeGenero;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Dtos.FilmeDto
{
    public class CriarFilmeDto
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage ="O campo Duracao é obrigatorio")]
        public int Duracao { get; set; }
        [Required(ErrorMessage = "O campo IdDiretor é obrigatorio")]
        public int DiretorId { get; set; }

        
       

    }
    
}
