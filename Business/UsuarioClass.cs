using Tareas.Data;
using Tareas.Models;

namespace Tareas.Business
{
    public class UsuarioClass
    {
        public static void CrearUsuario(UsuarioEntity usuario)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
            }
        }

    }
}
