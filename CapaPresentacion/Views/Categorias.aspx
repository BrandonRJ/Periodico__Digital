<%@ Page Title="Gestión de Categorías" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenido-wrapper">
        <div class="contenedor-seccion">
            <h3>Gestión de Categorías</h3>
            <hr />
            <div class="form-group">
                <label>Nombre de Categoría:</label>
                <asp:TextBox ID="txtNombreCat" runat="server" CssClass="form-control" placeholder="Ej. Deportes" />

                <%-- VALIDACIÓN: Impide guardar si el nombre de la categoría está vacío --%>
                <asp:RequiredFieldValidator ID="rfvCat" runat="server" ControlToValidate="txtNombreCat"
                    ErrorMessage="El nombre es obligatorio" CssClass="error" Display="Dynamic" />

                <label>Descripción Categoría:</label>
                <asp:TextBox ID="TextDescripcionmat" runat="server" CssClass="form-control" placeholder="Ej. descripción" />

                <%-- VALIDACIÓN: Obliga a ingresar una descripción para dar contexto a la categoría --%>
                <asp:RequiredFieldValidator ID="rfvmat" runat="server" ControlToValidate="TextDescripcionmat"
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
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre de Categoría" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion Categoría" />

                    <asp:TemplateField HeaderText="Noticias" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <span class="badge-conteo">
                                <%-- Muestra 0 si no hay noticias o el conteo total si existen --%>
                                <%# Eval("Noticias") == null ? "0" : DataBinder.Eval(Container.DataItem, "Noticias.Count") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <%-- CausesValidation="false" permite borrar sin que se disparen las alertas de los TextBox de arriba --%>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Eliminar"
                                CommandName="Delete" CssClass="btn-accion delete" CausesValidation="false"
                                OnClientClick="return confirm('¿Desea eliminar esta categoría?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
