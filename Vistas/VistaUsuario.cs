using System.ComponentModel.DataAnnotations;


namespace Tareas.vistas
{
    public class VistaUsuario
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre{ get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
        public string Apellidos { get; set; } = string.Empty;
        [Required(ErrorMessage="El campo Email es requerido. ")]
        public string Email { get; set; } = string.Empty;
                [Required(ErrorMessage = "El campo Password es obligatorio.")]
        public string Password1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Confirmación de Password es obligatorio.")]
        [Compare("Password1", ErrorMessage = "Los campos Password y su confirmación deben contener la misma información.")]
        public string Password2 { get; set; } = string.Empty;

    }
}
