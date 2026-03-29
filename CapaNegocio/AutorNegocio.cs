using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; // <--- AGREGAR ESTO PARA EL .INCLUDE

namespace Periodico_Digital.CapaNegocio
{
    public class AutorNegocio
    {
        // 1. LISTAR CON REPORTE (Punto 3.b)
        public List<Autor> ListarAutores()
        {
            using (var db = new PeriodicoContext())
            {
                // Agregamos .Include para que el GridView pueda contar las noticias
                return db.Autores.Include(a => a.Noticias).ToList();
            }
        }

        // 2. INSERTAR AUTOR
        public bool Insertar(string nombre)
        {
            using (var db = new PeriodicoContext())
            {
                // Si su entidad Autor tiene Email, recuerden agregarlo aquí
                db.Autores.Add(new Autor { Nombre = nombre });
                return db.SaveChanges() > 0;
            }
        }

        // 3. ELIMINAR AUTOR (Opcional pero recomendado para "Gestionar")
        public bool Eliminar(int id)
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

        internal object ObtenerReporteAutores()
        {
            throw new NotImplementedException();
        }

        internal bool RegistrarAutor(string nombre, string email)
        {
            throw new NotImplementedException();
        }

        internal bool EliminarAutor(int id)
        {
            throw new NotImplementedException();
        }
    }
}