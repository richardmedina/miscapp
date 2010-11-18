<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relations.aspx.cs" Inherits="Stprm.Web.Relations" MasterPageFile="~/Site.Master" %>
<%@ Register src="~/RelationsControl.ascx" TagName="RelationsControl" TagPrefix="med" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
<asp:UpdatePanel ID="panel" runat="server">
<ContentTemplate>
	<med:RelationsControl runat="server" ID="_rc_relations" />
</ContentTemplate>
</asp:UpdatePanel>

<ajax:UpdatePanelAnimationExtender
            id="upae1" BehaviorID="animation"
            TargetControlID="panel"
            runat="server">
        <Animations>
            <OnUpdating>
		<Parallel duration="0">
			<ScriptAction Script="document.getElementById('anim_id_search').style.visibility='visible';" />
		</Parallel>
            </OnUpdating>
            <OnUpdated>
		<Parallel duration="0">
		<ScriptAction Script="document.getElementById('anim_id_search').style.visibility='hidden';" />
		</Parallel>
            </OnUpdated>
        </Animations>
</ajax:UpdatePanelAnimationExtender>
</asp:Content>