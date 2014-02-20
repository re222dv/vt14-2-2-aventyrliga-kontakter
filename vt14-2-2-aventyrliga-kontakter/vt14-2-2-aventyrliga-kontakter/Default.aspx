<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="vt14_2_2_aventyrliga_kontakter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header>
            <h1>Äventyrliga kontakter</h1>
        </header>

        <asp:Panel ID="Correct" runat="server" Visible="false">
            <asp:Label ID="Label" runat="server" Text="Label">Kontakten sparades korrekt</asp:Label>
        </asp:Panel>

        <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="error" />
        <asp:ValidationSummary runat="server" CssClass="error" ValidationGroup="edit" />
        
        <asp:ListView ID="ListView" runat="server"
            ItemType="vt14_2_2_aventyrliga_kontakter.Model.Contact"
            SelectMethod="ListView_GetData"
            InsertMethod="ListView_InsertItem" InsertItemPosition="FirstItem"
            UpdateMethod="ListView_UpdateItem"
            DeleteMethod="ListView_DeleteItem"
            DataKeyNames="ContactID">
            <LayoutTemplate>
                <table>
                    <thead>
                        <tr>
                            <td>Förnamn</td>
                            <td>Efternamn</td>
                            <td>E-post</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Item.FirstName %></td>
                    <td><%# Item.LastName %></td>
                    <td><%# Item.EmailAddress %></td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick="return confirm('Är du säker på att du vill ta bort kontakten?');" />
                        <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td>Inga kontakter hittades :(</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr>
                    <td><asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>" MaxLength="50" /></td>
                    <td><asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>" MaxLength="50" /></td>
                    <td><asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>" MaxLength="50" TextMode="Email" /></td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" />
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                    </td>
                </tr>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Förnamn måste fyllas i" ControlToValidate="FirstName" Display="None" />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Efternamn måste fyllas i" ControlToValidate="LastName" Display="None" />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Epostadress måste fyllas i" ControlToValidate="EmailAddress" Display="None" />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="Ange en giltig epostadress" ControlToValidate="EmailAddress" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" />
            </InsertItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td><asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>" MaxLength="50" ValidationGroup="edit" /></td>
                    <td><asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>" MaxLength="50" ValidationGroup="edit" /></td>
                    <td><asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>" MaxLength="50" TextMode="Email" ValidationGroup="edit" /></td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Update" Text="Spara" ValidationGroup="edit" />
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                    </td>
                </tr>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Förnamn måste fyllas i" ControlToValidate="FirstName" Display="None" ValidationGroup="edit" />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Efternamn måste fyllas i" ControlToValidate="LastName" Display="None" ValidationGroup="edit" />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Epostadress måste fyllas i" ControlToValidate="EmailAddress" Display="None" ValidationGroup="edit" />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="Ange en giltig epostadress" ControlToValidate="EmailAddress" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" ValidationGroup="edit" />
            </EditItemTemplate>
        </asp:ListView>

        <asp:DataPager ID="DataPager" runat="server" PageSize="20" PagedControlID="ListView" >
            <Fields>
                <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowNextPageButton="false" PreviousPageText="<" FirstPageText="<<" />
                <asp:NumericPagerField ButtonType="Link" ButtonCount="10" />
                <asp:NextPreviousPagerField ShowLastPageButton="true" ShowPreviousPageButton="false" LastPageText=">>" NextPageText=">" />
            </Fields>
        </asp:DataPager>
    </div>
    </form>
</body>
</html>
