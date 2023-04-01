 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddGame.aspx.cs" Inherits="GamerzList.MainPage.AddGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
     <link rel="stylesheet" type="text/css" href="AddGame.css" /> 
     <link rel="website icon" type="png" href="../Images/My%20project%20(1).png"/> 
    <title></title>
</head>
<body style="background-color:black;">
    <form id="form1" runat="server" enctype="multipart/form-data">
  <div class="box">
    <p>
      <label for="FileUpload1">Game Picture:</label>
      <asp:FileUpload id="FileUpload1" runat="server" />
    </p>
    <p>
      <label for="GameTitle">Game Title:</label>
      <asp:TextBox id="GameTitle" runat="server" placeholder="Arth123"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Missing Information" ControlToValidate="GameTitle" runat="server" />
    </p>
    <p>
      <label for="Calendar1">Release Date:</label>
      <asp:Calendar ID="Calendar1" runat="server" Width="200px" style="margin: 0 auto;"></asp:Calendar>
      <asp:customvalidator runat="server" id="dateCustVal" onservervalidate="dateCustVal_ServerValidate"></asp:customvalidator>
    </p>
    <p>
      <label for="awardswon">Awards won:</label>
      <asp:DropDownList runat="server" ID="awardswon">
        <asp:ListItem Value="-1">Select here</asp:ListItem>
      </asp:DropDownList>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Missing Information" ControlToValidate="awardswon" runat="server" />
    </p>
    <p>
      <label for="gDescription">Description:</label>
      <asp:TextBox id="gDescription" runat="server" placeholder="youremail123@gmail.com"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Missing Information" ControlToValidate="gDescription" runat="server" />
    </p>
    <p runat="server" id="errormessage" style="color: red;"></p>
    <asp:Button runat="server" ID="Submit" Text="Submit" OnClick="Submit_Click" />
  </div>
</form>
</body>
</html>
