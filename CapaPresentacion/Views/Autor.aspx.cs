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
                // AJUSTE: Usamos DataKeyNames="IdAutor" como pusiste en tu diseño
                int id = Convert.ToInt32(gvAutores.DataKeys[e.RowIndex].Value);

                if (autorNegocio.EliminarAutor(id))
                {
                    CargarLista();
                    MostrarAlerta("Autor eliminado.");
                }
            }
            catch (Exception )
            {
                MostrarAlerta("No se puede eliminar porque tiene noticias asociadas.");
            }
        }

        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}