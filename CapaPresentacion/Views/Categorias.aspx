<%@ Page Title="Gestión de Categorías" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenido-wrapper">
        <div class="contenedor-seccion">
            <h3>Gestión de Categorías</h3>
            <hr />
            <div class="form-group">
                <label>Nombre de Categoría:</label>
                <asp:TextBox ID="txtNombreCat" runat="server" CssClass="form-control" placeholder="Ej. Deportes" />
                <asp:RequiredFieldValidator ID="rfvCat" runat="server" ControlToValidate="txtNombreCat"
                    ErrorMessage="La descripción es obligatoria" CssClass="error" Display="Dynamic" />
            </div>
            <div class="form-botones">
                <asp:Button ID="btnGuardarCat" runat="server" Text="Guardar Categoría"
                    OnClick="btnGuardarCat_Click" CssClass="btn btn-primary" />
            </div>
        </div>

        <div class="contenedor-seccion">
            <h3>Lista de Categorías</h3>
            <asp:GridView ID="gvCategorias" runat="server"
                AutoGenerateColumns="false"
                CssClass="tabla-inventario"
                DataKeyNames="Id"
                OnRowDeleting="gvCategorias_RowDeleting"
                GridLines="None">

                <HeaderStyle CssClass="tabla-header" />
                <AlternatingRowStyle CssClass="tabla-row-alt" />

                <Columns>
                    <asp:BoundField DataField="Descripcion" HeaderText="Nombre de Categoría" />

                    <asp:TemplateField HeaderText="Noticias" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <span class="badge-conteo">
                                <%# Eval("Noticias") == null ? "0" : DataBinder.Eval(Container.DataItem, "Noticias.Count") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Eliminar"
                                CommandName="Delete" CssClass="btn-accion delete"
                                OnClientClick="return confirm('¿Desea eliminar esta categoría?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>