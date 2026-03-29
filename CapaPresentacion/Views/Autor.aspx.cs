using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Periodico_Digital.CapaNegocio;
using Periodico_Digital.CapaDatos.Entidades;

namespace Periodico__Digital.CapaPresentacion.Views
{
    public partial class Autor : System.Web.UI.Page
    {
        private AutorNegocio autorNegocio = new AutorNegocio();

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
               
                gvAutores.DataSource = autorNegocio.ObtenerReporteAutores();
                gvAutores.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error al cargar: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // AJUSTE: Usamos los IDs de tu diseño (txtNombreAutor y txtEmailAutor)
                string nombre = txtNombreAutor.Text.Trim();
                string email = txtEmailAutor.Text.Trim();

                if (autorNegocio.RegistrarAutor(nombre, email))
                {
                    txtNombreAutor.Text = "";
                    txtEmailAutor.Text = "";
                    CargarLista();
                    MostrarAlerta("Autor guardado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message);
            }
        }

        protected void gvAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvAutores.DataKeys[e.RowIndex].Value);

                if (autorNegocio.EliminarAutor(id))
                {
                   
                    {
                        gvAutores.SelectedIndex = -1; // Limpia cualquier selección previa
                        CargarLista();
                        MostrarAlerta("Autor eliminado correctamente.");
                    }
                }
                else
                {
                    MostrarAlerta("No se encontró el autor para eliminar.");
                }
            }
            catch (Exception ex)
            {
                // REGLA DE ORO: Solo culpar a las noticias si el error viene de la base de datos
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MostrarAlerta("No se puede eliminar: el autor tiene noticias asociadas.");
                }
                else
                {
                    // Esto te dirá si el error es de conexión, de nulos, etc.
                    MostrarAlerta("Error técnico: " + ex.Message);
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