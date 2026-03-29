using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Periodico_Digital.CapaNegocio
{
    public class AutorNegocio
    {
        
        public List<Autor> ObtenerReporteAutores()
        {
            using (var db = new PeriodicoContext())
            {
                // Incluimos las noticias para poder contar cuántas tiene cada autor (Punto 3.b)
                return db.Autores.Include(a => a.Noticias).ToList();
            }
        }

        // 2. Registrar Autor (Resuelve el error de la línea 48 en tu imagen)
        public bool RegistrarAutor(string nombre, string email)
        {
            using (var db = new PeriodicoContext())
            {
                var nuevoAutor = new Autor
                {
                    Nombre = nombre,
                    Email = email
                };
                db.Autores.Add(nuevoAutor);
                return db.SaveChanges() > 0;
            }
        }

        // 3. Eliminar Autor (Resuelve el error de la fila deleting)
        public bool EliminarAutor(int id)
        {
            using (var db = new PeriodicoContext())
            {
                var autor = db.Autores.Find(id);
                if (autor != null)
                {
                    db.Autores.Remove(autor);
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }
    }
}