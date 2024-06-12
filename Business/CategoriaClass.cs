using Tareas.Data;
using Microsoft.EntityFrameworkCore;
using Tareas.Models;

namespace Tareas.Business
    {
    public class CategoriaClass
    {
        public static async Task<List<CategoriaEntity>> ListaCategoriasAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Categorias.OrderBy(p => p.NombreCategoria).ToListAsync();
            }
        }

        public static async Task<CategoriaEntity?> CategoriaPorIdAsync(int idCategoria)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Categorias.FindAsync(idCategoria); 
            }
        }
                
                public static void CrearCategoria(CategoriaEntity categoria)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();
            }
        }

        public static void ActualizaCategoria(CategoriaEntity categoria)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Categorias.Update(categoria);
                db.SaveChanges();
            }
        }

        public static async Task EliminaCategoriaAsync(int idCategoria )
        {
            using (var db = new ApplicationDbContext())
            {
                CategoriaEntity? categoria = new CategoriaEntity(); 
                categoria= await db.Categorias.FindAsync(idCategoria);
                if (categoria!=null)
                {
                    db.Categorias.Remove(categoria);
                    db.SaveChanges();
                }
            }
        }


    }
}
