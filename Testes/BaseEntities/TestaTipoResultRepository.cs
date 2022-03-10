using FluentResults;

namespace Testes.BaseEntities
{
    public static class TestaTipoResultRepository<T> where T : class
    {
        public static bool Retorna_FalseInFalid_TrueInSucess_Result(Result<T> resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }
    }
}
