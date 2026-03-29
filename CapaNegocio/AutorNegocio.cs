using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Periodico_Digital.CapaNegocio
{
    public class AutorNegocio
    {
        // 1. LISTAR CON REPORTE
        public List<Autor> ObtenerReporteAutores()
        {
            using (var db = new PeriodicoContext())
            {
                //Include para que el GridView pueda contar las noticias
                
                return db.Autores.Include(a => a.Noticias).ToList();
            }
        }

        // 2. REGISTRAR AUTOR
        public bool RegistrarAutor(string nombre, string email)
        {
            using (var db = new PeriodicoContext())
            {
                // Creamos el nuevo autor con los datos que vienen de la pantalla
                var nuevoAutor = new Autor
                {
                    Nombre = nombre,
                    Email = email 
                };

                db.Autores.Add(nuevoAutor);
                return db.SaveChanges() > 0;
            }
        }

        // 3. ELIMINAR AUTOR
        public bool EliminarAutor(int id)
        {
            using (var db = new PeriodicoContext())
            {
                var autor = db.Autores.Find(id);
                if (autor == null)
                    throw new Exception("El ID " + id + " no existe en la base de datos.");

                db.Autores.Remove(autor);
                return db.SaveChanges() > 0;
            }
        }

        public List<Autor> ListarAutores()
        {
            return ObtenerReporteAutores();
        }
    }
}