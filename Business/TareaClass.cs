using Tareas.Data;
using Tareas.Models;
using Microsoft.EntityFrameworkCore;
namespace Tareas.Business
{
    public class TareaClass
    {
        public static async Task<List<TareaEntity>> ListaTareasAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Tareas.Include(p => p.Categoria).OrderBy(p => p.Prioridad).ToListAsync();
            }
        }

        public static async Task<TareaEntity?> TareaPorIdAsync(int idTarea)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Tareas.FindAsync(idTarea);
            }
        }

        public static void CrearTarea(TareaEntity tarea)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Tareas.Add(tarea);
                db.SaveChanges();
            }
        }

        public static void ActualizaTarea(TareaEntity tarea)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Tareas.Update(tarea);
                db.SaveChanges();
            }
        }

        public static async Task  EliminaTareaAsync(int idTarea)
        {
            using (var db = new ApplicationDbContext())
            {
                TareaEntity? tarea = new TareaEntity();
                 tarea = await db.Tareas.FindAsync(idTarea);
                if (tarea!= null)
                {
                    db.Tareas.Remove(tarea);
                    db.SaveChanges();
                }
            }
        }


    }
}

