<%@ Page Title="MyWork" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyWork.aspx.cs" Inherits="CloudDev_POE.MyWork" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div>
               <div class="row">
		<asp:Repeater ID="productRepeater" runat="server">
			<ItemTemplate>
				<div class="column">
					<h2><%# Eval("name") %></h2>
					<asp:Image runat="server" ImageURL='<%# Eval("imageSRC") %>' Height="150" Width="150" />
					<p>
						<%# Eval("description") %>
					</p>
					<p>
						Price: R<%# Eval("price") %>
					</p>
				<asp:Button runat="server" Text="Add to Cart" ID="btnAddToCart" OnCommand="btnAddToCart_Command" OnClick="btnAddToCart_Click" CommandArgument='<%# Eval("productID") %>' />
				</div>
			</ItemTemplate>
		</asp:Repeater>

	</div>

	<style>
		.column {
			float: left;
			width: 33.33%;
			padding: 10px;
			height: 300px;
			height:fit-content;
		}

		.row::after {
			content: "";
			clear: both;
			display: table;
		}
	</style>
        </div>
</asp:Content>
