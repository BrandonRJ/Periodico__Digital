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
        private CategoriaNegocio catNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLista();
            }
        }

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

        protected void btnGuardarCat_Click(object sender, EventArgs e)
        {
            // Solo ejecutamos si los validadores pasaron
            if (Page.IsValid)
            {
                try
                {
                    string nombre = txtNombreCat.Text.Trim();
                    string descripcion = TextDescripcionmat.Text.Trim();

                    // Verificamos si estamos EDITANDO o CREANDO
                    if (ViewState["IdCategoriaEdicion"] != null)
                    {
                        // MODO EDICIÓN
                        int idCat = Convert.ToInt32(ViewState["IdCategoriaEdicion"]);

                        if (catNegocio.Actualizar(idCat, nombre, descripcion))
                        {
                            MostrarAlerta("¡Categoría actualizada con éxito!");
                            ViewState["IdCategoriaEdicion"] = null;
                            btnGuardarCat.Text = "Guardar Categoría";
                        }
                    }
                    else
                    {
                        // MODO INSERCIÓN
                        if (catNegocio.Insertar(nombre, descripcion))
                        {
                            MostrarAlerta("¡Guardado correctamente!");
                        }
                    }

                    // Limpieza y refresco (común para ambos)
                    txtNombreCat.Text = "";
                    TextDescripcionmat.Text = "";
                    CargarLista();
                }
                catch (Exception ex)
                {
                    // Este catch cierra el bloque try de la línea 41
                    MostrarAlerta("AVISO: " + ex.Message);
                }
            } 
        } 

        protected void gvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = gvCategorias.SelectedRow;

            // Al seleccionar, llenamos los campos
            txtNombreCat.Text = Server.HtmlDecode(fila.Cells[0].Text);
            TextDescripcionmat.Text = Server.HtmlDecode(fila.Cells[1].Text);

            // Guardamos el ID para que btnGuardarCat_Click sepa qué ID actualizar
            ViewState["IdCategoriaEdicion"] = gvCategorias.SelectedDataKey.Value;

            btnGuardarCat.Text = "Actualizar Categoría";
        }

        // Método de apoyo para limpiar los campos
        private void LimpiarFormulario()
        {
            txtNombreCat.Text = "";
            TextDescripcionmat.Text = "";
            ViewState["IdCategoriaEdicion"] = null; // Resetear modo edición
            btnGuardarCat.Text = "Guardar Categoría";
        }

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idCat = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);

                if (catNegocio.Eliminar(idCat))
                {
                    CargarLista();
                    MostrarAlerta("¡Listo! La categoría se eliminó.");
                }
            }
            catch (Exception ex)
            {
                CargarLista();
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MostrarAlerta("No se puede eliminar: Esta categoría tiene noticias asociadas.");
                }
                else
                {
                    MostrarAlerta("Error: No se pudo eliminar el registro.");
                }
            }
        }

        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}