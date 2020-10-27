<%@ Assembly Name="sp_tba_v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=80f44fc73453b537" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs" Inherits="sp_tba_v2.VisualWebPart1.VisualWebPart1UserControl" %>

<h1><asp:Label ID="WebPartTitleLabel" runat="server" Text="News"></asp:Label></h1>



<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="EmptyGridViewLabel" runat="server" Font-Size="24px" Text="There are no items to display."></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"></asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="AddNewsBtn" EventName="Click" />
     </Triggers>
</asp:UpdatePanel>

<div>
    <asp:Button ID="OpenPopupBtn" runat="server" Text="+" Style="margin: 10px;" OnClick="OpenPopupBtn_Click" />
</div>

<asp:Panel ID="Panel1" runat="server" Visible="False" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="410px" Style="margin: 10px; padding: 10px">
<div Style="margin: 5px;">
    <asp:Label ID="NewsTitleLabel" runat="server" Text="Title"></asp:Label>
    <asp:TextBox ID="NewsTitleTextBox" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
        ControlToValidate="NewsTitleTextBox"
        ErrorMessage="Title can't be empty."
        ForeColor="#bf0000">
    </asp:RequiredFieldValidator>
</div>
<div Style="margin: 5px;">
    <asp:Label ID="DescriptionLabel" runat="server" Text="Description"></asp:Label>
    <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
</div>
<div Style="margin: 5px;">
    <asp:Label ID="DatePublishingLabel" runat="server" Text="Date Publishing"></asp:Label>
    <SharePoint:DateTimeControl ID="DatePublishingDateTimeControl" runat="server" />
</div>
<div Style="margin: 5px;">
    <asp:Label ID="IsVisibleLabel" runat="server" Text="Is Visible"></asp:Label>
    <asp:CheckBox ID="IsVisibleCheckBox" runat="server" />
</div>
<div Style="margin: 5px;">
    <asp:Label ID="AssignedPersonLabel" runat="server" Text="Assigned Person"></asp:Label>
    <SharePoint:ClientPeoplePicker ValidationEnabled="true" ID="AssignedPersonPeoplePicker" runat="server" AutoFillEnabled="True" Rows="1" MultiSelect="false" AllowMultipleEntities="false" PrincipalAccountType="User"/>
</div>
<div Style="margin: 5px;">
    <asp:Button ID="AddNewsBtn" runat="server" Text="Add News" OnClick="AddNewsBtn_Click" />
</div>
</asp:Panel>
