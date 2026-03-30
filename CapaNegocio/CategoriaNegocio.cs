using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using System.Data.Entity;

namespace Periodico_Digital.CapaNegocio
{
    public class CategoriaNegocio
    {
        // 1. LISTAR  noticias para el conteo)
        public List<Categoria> ListarCategorias()
        {
            using (var db = new PeriodicoContext())
            {
                // El .Include("Noticias") es lo que hace que funcione el campo 'TotalNoticias'
                return db.Categorias.Include(c => c.Noticias).ToList();
            }
        }

        // 2. INSERTAR 
        public bool Insertar(string nombre, string descripcion)
        {
            using (var db = new PeriodicoContext())
            {
                // 1. Limpiamos los datos para que " Fútbol" sea igual a "fútbol"
                string n = nombre.Trim().ToLower();
                string d = descripcion.Trim().ToLower();

                // 2
                // SQL buscará: ¿Hay alguien que se llame 'n' Y que su descripción sea 'd'?
                bool existePareja = db.Categorias.Any(c =>
                    c.Nombre.ToLower() == n &&
                    c.Descripcion.ToLower() == d
                );

                if (existePareja)
                {
                    // Si la combinación ya existe, lanzamos el error de inmediato
                    throw new Exception("Ya existe el registro '" + nombre + "' con la descripción '" + descripcion + "'.");
                }

                // 3. Si no existe la combinación, se guarda normal
                var nueva = new Categoria
                {
                    Nombre = nombre.Trim(),
                    Descripcion = descripcion.Trim()
                };

                db.Categorias.Add(nueva);
                return db.SaveChanges() > 0;
            }
        }
        public bool Actualizar(int id, string nuevoNombre, string nuevaDescripcion)
        {
            using (var db = new PeriodicoContext())
            {
                // 1. Validar redundancia: ¿Hay OTRA categoría con el mismo nombre pero diferente ID?
                bool redundante = db.Categorias.Any(c =>
                    c.Nombre.ToLower() == nuevoNombre.Trim().ToLower() && c.Id != id);

                if (redundante)
                {
                    throw new Exception("No se puede actualizar: Ya existe otra categoría llamada '" + nuevoNombre + "'.");
                }

                // 2. Buscar la categoría por ID
                var cat = db.Categorias.Find(id);
                if (cat != null)
                {
                    // 3. Asignar los valores correspondientes
                    cat.Nombre = nuevoNombre.Trim();
                    cat.Descripcion = nuevaDescripcion.Trim();

                    // 4. Guardar y retornar true si hubo cambios
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }

        internal bool Eliminar(int idCat)
        {
            using (var db = new PeriodicoContext())
            {
                // 1. Buscamos la categoría real en la DB usando el ID
                var categoria = db.Categorias.Find(idCat);

                if (categoria != null)
                {
                    // 2. Si existe, la marcamos para borrar
                    db.Categorias.Remove(categoria);

                    // 3. Guardamos los cambios. Si se borró algo, devuelve true.
                    return db.SaveChanges() > 0;
                }

                return false; // No se encontró la categoría
            }
        }
    }
}