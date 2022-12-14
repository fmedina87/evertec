using EvertecPruebas.Domain.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EvertecPruebas.Domain.UserEntitys
{
    public class UsuarioRequestUpdate : Usuario
    {
        [Range(1, int.MaxValue, ErrorMessage = "El identificador del usuario no es válido. Por favor revise e intente nuevamente.")]       
        public override int IdUsuario { get; set; }
    }
}
