<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminBenefits.aspx.cs" Inherits="Stprm.Web.AdminBenefits" MasterPageFile="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/IdSearchControl.ascx" TagName="IdSearch" TagPrefix="med" %>
<%@ Register src="~/EmployeeInfo.ascx" TagName="EmployeeInfo" TagPrefix="med" %>
<%@ Register Src="~/Scholarship.ascx" TagName="Scholarship" TagPrefix="med" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<med:IdSearch ID="_isc_search" runat="server" />
<hr />
<med:EmployeeInfo runat="server" Id="_ei_employee" />
<hr />
Beneficio : <ajax:ComboBox runat="server" ID="_cmb_tipo" /> Beneficiario <asp:TextBox runat="server" ID="_txt_beneficiario" /> &nbsp; <asp:Button runat="server" Text="Agregar" />
<hr />
<asp:GridView runat="server" ID="_gv_benefits" CssClass="gridview" />
</asp:Content>

