<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="CloudDev_POE.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cart</h2>
    <asp:Label ID="lblEmpty" runat="server" Text="Your shopping cart is currently empty. Please add items to your cart from the work page." Visible="false" />
    <asp:DataGrid ID="dgCart" runat="server"></asp:DataGrid>
    <br />
    <br />
    <asp:Button ID="btnSubmitOrder" runat="server" OnClick="btnSubmitOrder_Click" Text="Complete Order" Visible="false" />
    <asp:Button ID="btnClearCart" runat="server" OnClick="btnClearCart_Click" Text="Clear Cart" Visible="false" />
</asp:Content>
