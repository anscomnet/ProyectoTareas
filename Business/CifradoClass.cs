using BCrypt;

namespace Tareas.Business
{
    public class CifradoClass
    {

        public static string CifraClave(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
                    }

        public static bool ChecaClave(string password, string cifrado)
        {
             bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, cifrado);
            return passwordMatch; 
        }

        public static DateTime expiraCodigo(DateTime fecha)
        {
            DateTime fechaExpiracion = fecha.AddMinutes(30);
            return fechaExpiracion;
        }


            }
}
