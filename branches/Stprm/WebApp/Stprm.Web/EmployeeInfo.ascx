<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeInfo.ascx.cs" Inherits="Stprm.Web.EmployeeInfo" %>


Ficha <asp:TextBox runat="server" ID="_txt_id" style="width: 70px;" />
Nombre <asp:TextBox runat="server" ID="_txt_name" style="width: 300px;" />
Situacion Contractual <asp:TextBox runat="server" ID="_txt_contr" style="width: 200px; color: blue;" />
<br />&nbsp;<br />
Plaza. <asp:TextBox runat="server" ID="_txt_posnum" style="width: 300px;"/>
Nivel. <asp:TextBox runat="server" ID="_txt_level" style="width: 20px;"/>
Clasificación. <asp:TextBox runat="server" ID="_txt_clasif" style="width: 220px"/>
Sección. <asp:TextBox runat="server" ID="_txt_section" style="width: 25px" />
<br />&nbsp;<br />
Categoría. <asp:TextBox runat="server" ID="_txt_category" style="width: 300px;" />
Jor. <asp:TextBox runat="server" ID="_txt_jor" style="width: 20px;" />
Depto. <asp:TextBox runat="server" ID="_txt_depto" style="width: 325px;" />
<br />&nbsp;<br />
Vigencia. <asp:TextBox runat="server" ID="_txt_validity_start" style="width: 200px;" />
al <asp:TextBox runat="server" ID="_txt_validity_end" style="width: 200px;" />
<asp:Button runat="server" ID="_btn_show_recompnl" Text="Asignar Recomendado" style="visibility: hidden;"/>

