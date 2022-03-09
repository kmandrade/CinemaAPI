using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Dtos.UsuarioDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.BaseEntities
{
    public static class TesteRepository
    {
       

        public static bool Retorna_FalseInFalid_TrueInSucess_Result(Result resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }
        public static bool Retorna_FalseInFalid_TrueInSucess_Ator(Result<LerAtorDto> resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }
        public static bool Retorna_FalseInFalid_TrueInSucess_Genero(Result<LerGeneroDto> resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }
        public static bool Retorna_FalseInFalid_TrueInSucess_Usuario(Result<LerUsuarioDto> resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }

        public static bool Retorna_FalseInFalid_TrueInSucess_Result_Filme(Result<LerNomeFilmeDto> resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }
    }
}
