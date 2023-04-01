<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="GamerzList.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
     <link rel="stylesheet" type="text/css" href="register.css" /> 
     <link rel="website icon" type="png" href="../Images/My%20project%20(1).png"/> 
    <title></title>
</head>
<body style="background-color:black">
    <form id="form1" runat="server">
        <div class="register">  
            <p>Profile Picture: <asp:FileUpload ID="FileUpload1" runat="server"/></p>
            <p>UserName: <asp:TextBox id="UserID" runat="server" placeholder="Arth123"></asp:TextBox></p>  
             <asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Missing Information" ControlToValidate="UserID" runat="server"/>  
            <p>First Name: <asp:TextBox id="FirstName" runat="server" placeholder="Arthur"></asp:TextBox></p>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Missing Information" ControlToValidate="FirstName" runat="server"/>  
            <p>Last Name: <asp:TextBox id="LastName" runat="server" placeholder="Morgan"></asp:TextBox></p>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Missing Information" ControlToValidate="LastName" runat="server"/>  
            <p style="text-align:inherit">   Email: <asp:TextBox id="uEmail" runat="server" placeholder="youremail123@gmail.com"></asp:TextBox></p>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Missing Information" ControlToValidate="uEmail" runat="server"/>  
            <p>Password: <asp:TextBox id="uPassw" runat="server" placeholder="yourpassword"></asp:TextBox></p>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Missing Information" ControlToValidate="uPassw" runat="server"/>  
            <p runat="server" id="errormessage" style="color:red;"></p> 
            <asp:Button runat="server" ID="Submit" Text="Register" OnClick="Submit_Click"/>
        </div>
    </form>
</body>
</html>
