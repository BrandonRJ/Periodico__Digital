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
            <asp:GridView ID="gvNoticias" runat="server" AutoGenerateColumns="false" CssClass="pagina-noticias" ShowHeader="false">

                <HeaderStyle CssClass="tabla-header" />
                <AlternatingRowStyle CssClass="tabla-row-alt" />

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="Noticias">
                                <!-- Titulo y autor -->
                                <div class="encabezado">
                                    <span class="noticia-titulo"><%# Eval ("Titulo") %></span>
                                    <br />
                                    <span class="noticia-autor">Creado por: <%# Eval ("Autor.Nombre") %></span>
                                </div>
                                
                                <!-- Contenido -->

                                <div class="contenido-noticia">
                                    <span class="noticia-contenido"><%# Eval ("Contenido") %></span>
                                </div>
                                
                                <!-- Parte inferior, categoria y fecha -->

                                <div class="informacion">
                                    <span class="noticia-categoria">Categoria: <%# Eval("Categoria.Descripcion") %></span>
                                    <br />
                                    <span class="noticia-fecha">Fecha de publicacion: <%# Eval("FechaPublicacion", "{0:dd/MM/yyyy}") %></span>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
