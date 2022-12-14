namespace EvertecPruebas.DataAcces.Interfaces
{
    public interface IRead<T> where T : class
    {
        Task<T> Get(int Id);
        Task<List<T>> Get();
    }
}
