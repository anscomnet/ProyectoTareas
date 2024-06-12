using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tareas.Models
{
    public class TareaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TareaId { get; set; }
        [Required(ErrorMessage ="El campo fecha programada es requerido.")]
        public DateTime FechaProgramada { get; set; }  
        [Required(ErrorMessage ="El nombre de la tarea es requerido.")]
        [StringLength(200)]
        public string NombreTarea { get; set; } = string.Empty;
        [Required(ErrorMessage ="El campo prioridad es requerido.")]
                public int Prioridad { get; set; }
        [StringLength(10)]
        public string Status { get; set; } = string.Empty;

                public int CategoriaId { get; set; }
        public CategoriaEntity ? Categoria { get; set; }
    }
}
