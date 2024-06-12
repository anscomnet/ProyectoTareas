using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tareas.Models
{
    public class UsuarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(50)]
        public string Apellidos { get; set; } = string.Empty;
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [StringLength(200)]
        public string Password { get; set; } = string.Empty;
        [StringLength(20)]
        public string Rol {  get; set; } = string.Empty;
    }
}
