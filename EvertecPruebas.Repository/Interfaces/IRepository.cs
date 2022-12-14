namespace EvertecPruebas.Repository.Interfaces
{
    public interface IRepository
    {
        public IUsuario Usuario { get; }
        public IEstadoCivil EsadoCivil { get; }
    }
}
