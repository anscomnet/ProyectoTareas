using Microsoft.EntityFrameworkCore;
using Tareas.Models;
namespace Tareas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<TareaEntity> Tareas { get; set; }
public DbSet<UsuarioEntity> Usuarios { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // Pruebas 
                options.UseSqlServer("Server = localhost\\SQLEXPRESS; Database=Tareas; Trusted_Connection=True; TrustServerCertificate=true;");
                            }
        }

    }
}
