<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Repeater.aspx.cs" Inherits="WebWithSql.Repeater" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/site.css" rel="stylesheet" />
</head>
<body>
    <form id="formShowDataWithRepeater" runat="server">
        <!-- Edit User Area -->
        <asp:Panel ID="panelEditUser" runat="server" Visible="false">
           <div>
            <table>
                <thead>
                    <tr>
                        <th>User Name:</th>
                        <th>E-mail</th>
                        <th>Date Of Birth</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <!-- New User Name -->
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                        <!-- E-mail -->
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                        <!-- Date Of Birth -->
                        <td>
                            <asp:TextBox ID="txtDateOfBirth" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnUpdateUser" runat="server" Text="Save User" OnClick="btnUpdateUser_Click" />&nbsp;
                            <asp:Button ID="btnCancelUserUpdate" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        </asp:Panel>
        <!-- End Of Edit User Area -->
        
        <div>
            <asp:Label ID="lblError" runat="server" CssClass="errorMsg"></asp:Label>
        </div>
        <!-- Show User Data -->
        <div>
            <asp:Repeater ID="repeatUsers" runat="server" DataSourceID="dataSourceUsers" OnItemCommand="repeatUsers_ItemCommand">
                <HeaderTemplate>
                    <table class="repeaterUserTable">
                        <thead>
                            <tr>
                                <th>User Name</th>
                                <th>E-Mail</th>
                                <th>Date Of Birth</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("BirthData") %></td>
                        <td>
                            <asp:ImageButton 
                                ID="btnEditUser" 
                                runat="server" 
                                CommandName="edit" 
                                CommandArgument=<%# Eval("UserID") %>
                                ImageUrl="~/Images/edit.jpg" />&nbsp;
                            <asp:ImageButton 
                                ID="btnDeleteUser" 
                                runat="server" 
                                CommandName="delete"
                                CommandArgument=<%# Eval("UserID") %>
                                ImageUrl="~/Images/delete.jpg" />
                        </td>
                    </tr>
                </ItemTemplate>

                <AlternatingItemTemplate>
                    <tr class="alternatingRow">
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("BirthData") %></td>
                        <td>
                            <asp:ImageButton 
                                ID="btnEditUser" 
                                runat="server" 
                                CommandName="edit" 
                                CommandArgument=<%# Eval("UserID") %>
                                ImageUrl="~/Images/edit.jpg" />&nbsp;
                            <asp:ImageButton 
                                ID="btnDeleteUser" 
                                runat="server" 
                                CommandName="delete"
                                CommandArgument=<%# Eval("UserID") %>
                                ImageUrl="~/Images/delete.jpg" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>

                <FooterTemplate>
                        </tbody>
                        <thead>
                            <tr>
                                <th>User Name</th>
                                <th>E-Mail</th>
                                <th>Date Of Birth</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!-- End Of Show User Data -->
        <asp:SqlDataSource
            ID="dataSourceUsers"
            runat="server"
            ConnectionString='<%$ ConnectionStrings:CompanyConnectionString %>'
            SelectCommand="SELECT * FROM [Users]" 
            DeleteCommand="DELETE FROM [Users] WHERE [UserID] = @UserID" 
            InsertCommand="INSERT INTO [Users] ([UserName], [Email], [BirthData]) VALUES (@UserName, @Email, @BirthData)" 
            UpdateCommand="UPDATE [Users] SET [UserName] = @UserName, [Email] = @Email, [BirthData] = @BirthData WHERE [UserID] = @UserID">
            <DeleteParameters>
                <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="UserName" Type="String"></asp:Parameter>
                <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                <asp:Parameter Name="BirthData" Type="DateTime"></asp:Parameter>
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="UserName" Type="String"></asp:Parameter>
                <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                <asp:Parameter Name="BirthData" Type="DateTime"></asp:Parameter>
                <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>