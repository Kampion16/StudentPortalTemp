<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet/Login.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style=" margin: 0 auto">
            <tr>
                <td>
                   
                </td>
            </tr>
        
            </table>
    <div id="login_form"  >
        
     <table style="margin: 0 auto; padding-top:50px; ">
         <tr>
             <td> <!-- #include file=Includes/header.htm --></td>
         </tr>
         
         </table>
        <div style="width:180px;margin-left:25px">
       <asp:Label ID="errmsg" runat="server"></asp:Label>
            </div>
        <table  style=" margin: 0 auto; padding-top:50px; width:370px;" >
         <tr>
             <td>
                 <div class="formGroup"  >
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Username cannot be empty"></asp:RequiredFieldValidator>
                     <asp:TextBox ID="txtName" class="form-control" placeholder="Username"  runat="server" ></asp:TextBox><i class="fa fa-user"></i>
                 </div>
                 
        
             </td>
         </tr>
         <tr>
             <td>
                 <div class="formGroup" >
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password cannot be empty"></asp:RequiredFieldValidator>     <asp:TextBox ID="txtPassword" class="form-control" placeholder="Password"  runat="server" ></asp:TextBox> <i class="fa fa-lock"></i>
                      </div>
        <p></p>
                  
        
        <p><asp:Button ID="btnLogin" class="log-btn" runat="server" Text="Log in"  OnClick="btnLogin_Click" /></p>
            
             </td>
         </tr>
     </table>
         
        
        <%--<p><asp:Button ID="btnLogin" runat="server" Text="Login" /></p>--%>
        
    </div>
         <table align="center"  border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; background-color: #ffffff">
                <tr>
                    <td>
                        <img src="images/Footer Strip_980x143px.jpg" width="980" height="143" usemap="#Map" border="0" /></td>
                </tr>
            </table>
           <map name="Map" id="Map">
                <area shape="rect" coords="21,37,90,91" href="http://intranet.dwa.gov.za/" target="_blank" />
                <area shape="rect" coords="23,94,90,138" href="http://intranet.dwa.gov.za/webmail.aspx" target="_blank" />
                <area shape="rect" coords="127,21,210,127" href="http://www.dwa.gov.za/events/events2019.aspx" target="_blank" />
                <area shape="rect" coords="224,22,307,129" href="http://www.dwa.gov.za/minister/minSpeeches.aspx" target="_blank" />
                <area shape="rect" coords="313,24,420,130" href="http://www.dwa.gov.za/Comms/MediaStatements.aspx?year=2019" target="_blank" />
                <area shape="rect" coords="423,25,508,130" href="http://www.dwa.gov.za/Hydrology/" target="_blank" />
                <area shape="rect" coords="539,13,599,72" href="http://www.facebook.com/pages/Department-of-Water-and-Sanitation/358792120941804?" target="_blank" />
                <area shape="rect" coords="538,74,599,129" href="https://www.youtube.com/channel/UC7-_vH_w7tdaakc0zvn2e6Q" target="_blank" />
                <area shape="rect" coords="603,14,664,69" href="https://www.twitter.com/DWS_RSA" target="_blank" />
                <area shape="rect" coords="605,72,662,129" href="http://instagram.com/water_and_sanitation_rsa " target="_blank" />
            </map>
    </form>
</body>
</html>
