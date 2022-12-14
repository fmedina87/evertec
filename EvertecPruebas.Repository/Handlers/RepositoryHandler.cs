using EvertecPruebas.DataAcces.Interfaces;
using EvertecPruebas.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace EvertecPruebas.Repository.Handlers
{
    public class RepositoryHandler : IRepository
    {
        public IUsuario Usuario { get; }
        public IEstadoCivil EsadoCivil { get; }       
        public RepositoryHandler(IDbContext context, ILogger<UsuarioHandler> logger)
        {
            Usuario = new UsuarioHandler(context, this, logger);
            EsadoCivil = new EstadoCivilHandler(context);
        }
    }
}
