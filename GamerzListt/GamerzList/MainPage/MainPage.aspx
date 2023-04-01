<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="GamerzList.MainPage.MainPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="MainPage.css" />
    <link rel="website icon" type="png" href="../Images/My%20project%20(1).png"/>
    <title></title>
</head>
<body style="background-color:black;">
    <form id="form1" runat="server">
        <header class="site-header sticky-top py-1">
            <nav class="container d-flex flex-column flex-md-row justify-content-between navbar">
                <a class="py-2" href="../UserPage/UserInfo.aspx" aria-label="UserPage" runat="server">
                    <asp:ImageButton ID="userPfp" runat="server" ImageUrl="path/to/image.png" PostBackUrl="../UserPage/UserInfo.aspx" Height="50" Width="50" />
                </a>
                <a class="py-2 d-none d-md-inline-block nav-link" href="AddGame.aspx" aria-label="AddGame" runat="server">
                    <asp:LinkButton runat="server" Text="Add Game" CssClass="nav-link" OnClick="Unnamed_Click2"/>
                </a>
                <a class="py-2 d-none d-md-inline-block nav-link" href="~/LogIn/LogIN.aspx" aria-label="LogOut" runat="server">
                    <asp:LinkButton ID="LogoutButtonNav" runat="server" Text="Log Out" CssClass="nav-link" OnClick="LogoutButtonNav_Click"/>
                </a>
            </nav>
      </header> 
          <div class="container mt-4">
                <h4>Write a Post:</h4>
                <asp:TextBox ID="txtPostTitle" runat="server" CssClass="form-control mb-2" MaxLength="50" />
                <asp:TextBox ID="txtPostContent" runat="server" TextMode="MultiLine" CssClass="form-control mb-2" Rows="3" MaxLength="280" />
                <asp:Button ID="btnSubmitPost" runat="server" Text="Submit Post" CssClass="btn btn-primary" OnClick="btnSubmitPost_Click" />
                <asp:Label ID="lblPostMessage" runat="server" CssClass="mt-2" ForeColor="Red" />
            </div>
        <br />
        <div class="container">
            <div class="row">
                <asp:Repeater ID="PostRepeater" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="card mb-4">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Title") %></h5>
                                    <p class="card-text"><%# Eval("Content") %></p>
                                    <p class="card-text">
                                        <small class="text-muted">Posted by <%# Eval("UserId") %> on <%# Eval("DateCreated", "{0:g}") %></small>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
  </body>
</html>