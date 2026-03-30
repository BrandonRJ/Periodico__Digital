<%@ Page Title="Gestión de Noticias" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Noticias.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenido-wrapper">
        <div class="contenedor-seccion">
            <h2>Gestión de Noticias</h2>
            <div class="form-group">
                <label>Noticia:</label>
                <asp:TextBox ID="txtNoticia" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label>Contenido:</label>
                <asp:TextBox ID="txtContenido" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
            <div class="form-group">
                <label>Autor:</label>
                <asp:DropDownList ID="ddlAutor" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label>Categoría:</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label>Estado de Publicación:</label>
                <asp:RadioButtonList ID="rblEstado" runat="server">
                    <asp:ListItem Text="Publicada" Value="Publicada" Selected="True" />
                    <asp:ListItem Text="Borrador" Value="Borrador" />
                    <asp:ListItem Text="Archivada" Value="Archivada" />
                </asp:RadioButtonList>
            </div>
            <div class="form-botones">
                <asp:Button ID="btnGuardarNoticia" runat="server" Text="Guardar Noticia" OnClick="btnGuardarNoticia_Click" CssClass="btn btn-primary" />
            </div>
        </div>
        <div class="contenedor-seccion">
            <asp:GridView ID="gvNoticias" runat="server" AutoGenerateColumns="false" CssClass="tabla-inventario">
                <HeaderStyle CssClass="tabla-header" />
                <AlternatingRowStyle CssClass="tabla-row-alt" />
                <Columns>
                    <asp:BoundField DataField="Titulo" HeaderText="Noticia" />
                    <asp:BoundField DataField="Autor.Nombre" HeaderText="Autor" />
                    <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" />
                    <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
