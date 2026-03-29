using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Periodico_Digital.CapaNegocio; 
using Periodico_Digital.CapaDatos.Entidades;

namespace Periodico__Digital.CapaPresentacion.Views
{
    public partial class Categorias : System.Web.UI.Page
    {
        // Instancia de la capa de negocio
        private CategoriaNegocio catNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLista();
            }
        }

        // Método para llenar el GridView
        private void CargarLista()
        {
            try
            {
                gvCategorias.DataSource = catNegocio.ListarCategorias();
                gvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error al cargar la lista: " + ex.Message);
            }
        }

        // Evento del botón Guardar (Cumple Punto 3.c)
        protected void btnGuardarCat_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string descripcion = txtNombreCat.Text.Trim();

                    if (catNegocio.Insertar(descripcion))
                    {
                        txtNombreCat.Text = ""; // Limpiar campo
                        CargarLista(); // Refrescar tabla
                        MostrarAlerta("Categoría guardada con éxito.");
                    }
                }
                catch (Exception ex)
                {
                    MostrarAlerta("Error al guardar: " + ex.Message);
                }
            }
        }

        // Evento para Eliminar (Cumple con 'Gestionar')
        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Obtenemos el ID de la fila seleccionada
                int idCat = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);

                if (catNegocio.Eliminar(idCat))
                {
                    CargarLista();
                    MostrarAlerta("Categoría eliminada correctamente.");
                }
            }
            catch (Exception)
            {
                // Importante: Si la categoría tiene noticias, EF lanzará un error de FK
                MostrarAlerta("No se puede eliminar la categoría porque tiene noticias vinculadas.");
            }
        }

        // Métodos de apoyo
        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}