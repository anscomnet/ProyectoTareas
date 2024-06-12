using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace    Tareas.Models
{
    public class CategoriaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int CategoriaId { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage ="El campo Nombre de la categoría es requerido.")]
        public string NombreCategoria { get; set; } = string.Empty;
        
                public ICollection<TareaEntity> ? Tareas { get; set; }

    }
}
