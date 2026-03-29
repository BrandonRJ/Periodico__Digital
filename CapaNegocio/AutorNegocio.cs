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
                // VALIDACIÓN: ¿Ya existe un autor con este correo?
                // Usamos .Any() que es muy rápido porque solo devuelve true/false
                bool existe = db.Autores.Any(a => a.Email.ToLower() == email.ToLower());

                if (existe)
                {
                    // Lanzamos una excepción que capturaremos en la Vista 
                    throw new Exception("Ya existe un autor registrado con ese correo electrónico.");
                }
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

        internal bool ActualizarAutor(int id, string nombre, string email)
        {
            // 1. Usamos el contexto de la base de datos
            using (var db = new PeriodicoContext())
            {
                // 2. Buscamos al autor original por su ID
                var autorExistente = db.Autores.Find(id);

                if (autorExistente != null)
                {
                    // 3. Modificamos sus propiedades con los nuevos datos
                    autorExistente.Nombre = nombre;
                    autorExistente.Email = email;

                    // 4. Guardamos los cambios. SaveChanges() devuelve el número de filas afectadas.
                    // Si es mayor a 0, significa que la actualización fue exitosa.
                    return db.SaveChanges() > 0;
                }

                return false; // No se encontró el autor
            }
        }
    }
}
        