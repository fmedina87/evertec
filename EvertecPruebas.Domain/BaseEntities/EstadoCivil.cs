using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvertecPruebas.Domain.BaseEntities
{
    [Table("EstadoCivil")]
    public class EstadoCivil
    {
        [Key]
        [Column("IdEstadoCivil")]
        public int IdEstadoCivil { get; set; }
        [Column("Nombre")]
        public string NombreEstadoCivil { get; set; }
        public EstadoCivil()
        {
            NombreEstadoCivil = string.Empty;
        }
    }
}
