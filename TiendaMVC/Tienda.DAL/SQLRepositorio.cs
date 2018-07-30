using System.Data.Entity;
using System.Linq;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;

namespace Tienda.DAL
{
    public class SQLRepositorio<T> : IRepositorio<T> where T : EntidadBase
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        public SQLRepositorio(DataContext context) {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Eliminar(string Id)
        {
            var t = Buscar(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public T Buscar(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insertar(T t)
        {
            dbSet.Add(t);
        }

        public void Actualizar(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
