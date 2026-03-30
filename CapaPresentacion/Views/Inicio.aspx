<%@ Page Title="" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenido-wrapper">
        <h2>Pagina de inicio</h2>
        <br />
        <div class="ddl-group">
            <asp:DropDownList ID="ddlAutor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAutor_SelectedIndexChanged">
                
            </asp:DropDownList>

            <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                
            </asp:DropDownList>
        </div>


        <div class="contenedor-seccion">
            <asp:GridView ID="gvNoticias" runat="server" AutoGenerateColumns="false" CssClass="tabla-inventario">
                <HeaderStyle CssClass="tabla-header" />
                <AlternatingRowStyle CssClass="tabla-row-alt" />
                <Columns>
                    <asp:BoundField DataField="Titulo" HeaderText="Título" />
                    <asp:BoundField DataField="Autor.Nombre" HeaderText="Autor" />
                    <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" />
                    <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
