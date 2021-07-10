<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Eventype.aspx.cs" Inherits="Eventype" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <table style="margin: 0 auto; width: 1024px">
            <tr>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <h1 style="text-align: center">Add Event Type</h1>
                    <p style="text-align: center">Please provide details of the Event:</p>
                </td>
            </tr>
        </table>

        <table style="margin: 0 auto; border: solid 1px; width: 600px">
            <tr>
                <td>

         <table style="margin: 0 auto; table-layout: fixed; background-color: white; width: 600px">
                        <tr>
                            <td class="auto-style2">Event Name:  <span class="spanRed">*</span>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="EventName" /><p></p>

                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="vname" runat="server" ControlToValidate="EventName" ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>

                            <td class="auto-style2">Event Description:  <span class="spanRed">*</span>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="EventDes" /><p></p>

                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EventDes" ErrorMessage="Event description cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ready" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
          <table style="margin: 0 auto;">
            <tr>
                <td>
                    <%--<div class="button" style="width: 130px; height: 40px; text-align: center; vertical-align: middle">
                        <a href="Default - copy.aspx">Cancel</a>
                    </div>--%>

                </td>
                <td>
                    <div class="button" style="width: 130px; height: 40px; text-align: center; vertical-align: middle">
                        <asp:Button ID="btn5" runat="server" Style="background-color: green; border: none; color: white; font-size: 16px;" OnClick="btn5_Click" Text="Submit"></asp:Button>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
