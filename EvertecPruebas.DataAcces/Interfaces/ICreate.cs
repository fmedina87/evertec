namespace EvertecPruebas.DataAcces.Interfaces
{
    public interface ICreate<T,Y> where T : class
    {
        Task<Y> Add(T NuevoObjeto);
    }
}
