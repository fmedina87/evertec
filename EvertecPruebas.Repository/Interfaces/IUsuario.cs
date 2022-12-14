using EvertecPruebas.DataAcces.Interfaces;
using EvertecPruebas.Domain.BaseEntities;
using EvertecPruebas.Domain.UserEntitys;

namespace EvertecPruebas.Repository.Interfaces
{
    public interface IUsuario : ICreate<Usuario, UsuarioResponse>, IDelete, IRead<UsuarioResponse>, IUpdate<Usuario>
    {
    }
}
