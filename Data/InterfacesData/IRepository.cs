namespace Data.InterfacesData
{
    public interface IRepository<T>
    {

        Task<IEnumerable<T>> BuscarTodos();

        Task<T> BuscarPorId(int id);



        Task Cadastrar(T obj);
        Task Alterar(T obj);
        void Excluir(T obj);
        Task<int> Save();


    }
}
