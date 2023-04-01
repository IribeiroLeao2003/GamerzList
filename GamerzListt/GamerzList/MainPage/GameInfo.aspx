<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameInfo.aspx.cs" Inherits="GamerzList.MainPage.GameInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <link rel="website icon" type="png" href="../Images/My%20project%20(1).png"/>  
    <link rel="stylesheet" type="text/css" href="GameInfo.css" />
    <title></title>
</head>
<body style="background-color:black;">
    <form id="form1" runat="server">
        
        <div class="userBox">
          <asp:ImageButton ID="userPfp" runat="server" Width="50px" Height="50px" OnClick="userPfp_Click"/>
          <p runat="server" id="accountName" style="position: relative; bottom: 50px; left:65px; padding-right: 60px;"></p> 
          <asp:Button OnClick="Unnamed_Click" runat="server" Text="🠻" CssClass="dropbtn" Width="50px" Height="45px"/>
        </div> 


        <div class="showinfo"> 
            <asp:Image runat="server"/>  <p runat="server" id="gameName"></p>
            
            
            
        </div>
    </form>
</body>

</html>
