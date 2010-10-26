<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminBenefits.aspx.cs" Inherits="Stprm.Web.AdminBenefits" MasterPageFile="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/IdSearchControl.ascx" TagName="IdSearch" TagPrefix="med" %>
<%@ Register src="~/EmployeeInfo.ascx" TagName="EmployeeInfo" TagPrefix="med" %>
<%@ Register Src="~/Scholarship.ascx" TagName="Scholarship" TagPrefix="med" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<asp:UpdatePanel runat="server" ID="_pnl_search">
<ContentTemplate>
<med:IdSearch ID="_isc_search" runat="server" />
<hr />
<med:EmployeeInfo runat="server" Id="_ei_employee" />
<hr />

</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel runat="server" ID="_up_win">
<ContentTemplate>
<asp:Button runat="server" ID="_btn_popup" Text="Mostrar Popup" />

<asp:Panel ID="_panel" runat="server" Style="border: solid 1px red; background: #FFFFFF;" Width="200px" Height="200px" >
Tipo apoyo: <ajax:ComboBox runat="server" ID="_cmb_apoyos" />

<asp:Button ID="_btn_popup_close" runat="server" Text="Cerrar" />
</asp:Panel>

<ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="_btn_popup" PopupControlID="_panel" OkControlID="_btn_popup_close" runat="server" RepositionMode="RepositionOnWindowResizeAndScroll" />
</ContentTemplate>
</asp:UpdatePanel>

<ajax:UpdatePanelAnimationExtender
            id="upae1" BehaviorID="animation"
            TargetControlID="_pnl_search"
            runat="server">
        <Animations>
            <OnUpdating>
		<Parallel duration="0">
			<ScriptAction Script="document.getElementById('_loading').style.visibility='visible';" />
			<EnableAction AnimationTarget="_isc_search" Enabled="false" />
			
		</Parallel>
            </OnUpdating>
            <OnUpdated>
		<Parallel duration="0">
			<ScriptAction Script="document.getElementById('_loading').style.visibility='hidden';" />
			<EnableAction AnimationTarget="_isc_search" Enabled="true" />
		</Parallel>
            </OnUpdated>
        </Animations>
</ajax:UpdatePanelAnimationExtender>

</asp:Content>

