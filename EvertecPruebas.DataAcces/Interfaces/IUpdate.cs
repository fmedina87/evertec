namespace EvertecPruebas.DataAcces.Interfaces
{
    public interface IUpdate<T> where T : class
    {
        Task Update(T ObjetoModificado);
    }
}
