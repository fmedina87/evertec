using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvertecPruebas.Domain.BaseEntities
{
    [Table("Usuario")]
    public class Usuario
    {

        [JsonIgnore]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int IdUsuario { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El identificador del estado civil es obligatorio")]
        public int IdEstadoCivil { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Campo de solo letras")]
        [Required(ErrorMessage = "primer nombre es obligatorio.")]
        public string PrimerNombre { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Campo de solo letras")]
        public string? SegundoNombre { get; set; }
        [Required(ErrorMessage = "primer apellido es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Campo de solo letras")]
        public string PrimerApellido { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Campo de solo letras")]
        public string? SegundoApellido { get; set; }
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/1900", "31/12/3999", ErrorMessage = "El valor para {0} debe estar en un rango entre {1} y {2}")]
        public DateTime FechaNacimiento { get; set; }
        public bool TieneHermanos { get; set; }
        public Usuario()
        {
            PrimerNombre = string.Empty;
            SegundoNombre = string.Empty;
            PrimerApellido = string.Empty;
            SegundoApellido = string.Empty;
        }
    }
}
