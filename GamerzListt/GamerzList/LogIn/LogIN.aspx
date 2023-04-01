<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIN.aspx.cs" Inherits="GamerzList.LogIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <link rel="stylesheet" type="text/css" href="LogIn.css" /> 
    <link rel="website icon" type="png" href="../Images/My%20project%20(1).png"/> 
    <title></title>
</head>
<body style="background-color:black; background-image:url('../Images/nova-filepond-9d9mwS.jpg '); background-size: cover; background-position: center;">
    <form id="form1" runat="server"> 
         <div class="mainlog">  
            <br /> 
            <img src="../Images/SeekPng.com_game-controller-png_8209893.png" style=" width: 150px;"/>
            <br /> 
            <br />
            <asp:TextBox ID="UserName" runat="server"  placeholder="username" CssClass="styled1"></asp:TextBox> 
            <asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Please enter Username" ControlToValidate="UserName" runat="server"/>  
            <asp:TextBox ID="UserPassword" runat="server"  placeholder="password" CssClass="styled1"></asp:TextBox> 
            <asp:RequiredFieldValidator ID="rfvPassword" ErrorMessage="Please enter Password" ControlToValidate="UserPassword" runat="server" />  
            <br />   
            <p runat="server" id="errormessage" style="color:red;"></p> 
            <br />  
            <asp:Button runat="server" Text="Log In" OnClick="Unnamed_Click" CssClass="button"/>  
            <p style="text-align:left">Dont have a <br /> account ? <asp:HyperLink ID="HyperLink1" NavigateUrl="register.aspx" runat="server">Register Now!</asp:HyperLink></p> 
            <asp:LinkButton ID="StayUser1"  runat="server" OnClick="StayUser1_Click" CausesValidation="false">Stay as User?</asp:LinkButton>
        </div> 
    </form>
</body>
