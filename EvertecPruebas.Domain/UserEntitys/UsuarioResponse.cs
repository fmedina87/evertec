using EvertecPruebas.Domain.BaseEntities;

namespace EvertecPruebas.Domain.UserEntitys
{
    public class UsuarioResponse : Usuario
    {
        public override int IdUsuario { get; set; }
        public string EstadoCivil { get; set; }
        public UsuarioResponse()
        {
            EstadoCivil = string.Empty;
        }
    }
}
