<%@ Page Title="" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Noticias.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Noticias</h2>
        <div class="card p-4 shadow-sm">
            <div class="row">
                <div class="col-md-6">
                    <label>Título de la Noticia:</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" Required="true" />

                    <label class="mt-2">Contenido:</label>
                    <asp:TextBox ID="txtContenido" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                </div>

                <div class="col-md-3">
                    <label>Autor:</label>
                    <asp:DropDownList ID="ddlAutor" runat="server" CssClass="form-select"></asp:DropDownList>

                    <label class="mt-2">Categoría:</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label>Estado de Publicación:</label>
                    <asp:RadioButtonList ID="rblEstado" runat="server" CssClass="mt-2">
                        <asp:ListItem Text="Publicada" Value="Publicada" Selected="True" />
                        <asp:ListItem Text="Borrador" Value="Borrador" />
                        <asp:ListItem Text="Archivada" Value="Archivada" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="mt-3">
                <asp:Button ID="btnGuardarNoticia" runat="server" Text="Guardar Noticia"
                    OnClick="btnGuardarNoticia_Click" CssClass="btn btn-primary" />
            </div>
        </div>

        <div class="mt-4">
            <asp:GridView ID="gvNoticias" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                <Columns>
                    <asp:BoundField DataField="Titulo" HeaderText="Título" />
                    <asp:BoundField DataField="Autor.Nombre" HeaderText="Autor" />
                    <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" />
                    <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
