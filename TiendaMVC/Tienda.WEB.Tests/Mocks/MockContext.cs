using System;
using System.Collections.Generic;
using System.Linq;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;

namespace Tienda.WEB.Tests.Mocks
{
    public class MockContext<T> : IRepositorio<T> where T: EntidadBase
    {
        List<T> items;
        string NombreClase;

        public MockContext()
        { 
            items = new List<T>();
        }

        public void Commit()
        {
            return;
        }

        public void Insertar(T t)
        {
            items.Add(t);
        }

        public void Actualizar(T t)
        {
            T tHaActualizar = items.Find(i => i.Id == t.Id);

            if (tHaActualizar != null)
            {
                tHaActualizar = t;
            }
            else
            {
                throw new Exception(NombreClase + " Not found");
            }
        }

        public T Buscar(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(NombreClase + " No encontrado");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Eliminar(string Id)
        {
            T tHaEliminar = items.Find(i => i.Id == Id);

            if (tHaEliminar != null)
            {
                items.Remove(tHaEliminar);
            }
            else
            {
                throw new Exception(NombreClase + " No encontrado");
            }
        }

    }
}

