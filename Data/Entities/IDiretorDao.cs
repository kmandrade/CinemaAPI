﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IDiretorDao :IRepository<Diretor>
    {
        IEnumerable<Filme> BuscaFilmesPorDiretor(Diretor diretor);
    }
}