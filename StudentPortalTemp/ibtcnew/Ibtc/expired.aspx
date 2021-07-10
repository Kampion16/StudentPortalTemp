<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expired.aspx.cs" Inherits="expired" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
        <link rel="stylesheet" type="text/css" href="StyleSheet/BookVenuT.css" />  
<script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>

     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

  <link rel="stylesheet" href="/resources/demos/style.css" />    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1024px; margin:  auto 0">
    <h1 style="font-size:45px; text-align: center ">Session has Expired. Please Login!</h1>
        <asp:Button  runat="server" OnClick="redirect" Text="Login"  class="loginBtn"></asp:Button>
    </div>


    </form>
</body>
</html>
