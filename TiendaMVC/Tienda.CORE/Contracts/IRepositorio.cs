using System.Linq;
using Tienda.CORE.Models;

namespace Tienda.CORE.Contracts
{
    public interface IRepositorio<T> where T : EntidadBase
    {
        IQueryable<T> Collection();
        void Commit();
        void Eliminar(string Id);
        T Buscar(string Id);
        void Insertar(T t);
        void Actualizar(T t);
    }
}