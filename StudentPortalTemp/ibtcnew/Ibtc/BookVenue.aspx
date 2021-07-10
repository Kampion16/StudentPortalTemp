<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookVenue.aspx.cs" Inherits="BookVenue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2 style="text-align:center">
            VENUE RESERVATION FORM
		IBTC FACILITIES 

        </h2>
        <h3>
(Please note: venue bookings must be done 14 working days before the event)
        </h3>
    <table>
        <tr>
            <td>Department/Client:</td>
            <td> <asp:TextBox ID="txtName"  runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Directorate:</td>
            <td> <asp:TextBox ID="TextBox1"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Branch:</td>
            <td> <asp:TextBox ID="TextBox2"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Name of Event:</td>
            <td> <asp:TextBox ID="TextBox3"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>No. of People:</td>
            <td> <asp:TextBox ID="TextBox4"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Type of Facility Required (√) appropriate block/s</td>
            <td>
                <table>
                    <tr>
                        <td>Auditorium 60 max</td>
                        <td>
                            Classroom 16-18 max p/class x 3
                        </td>
                        <td>
                            Boardroom 25 max
                        </td>
                        <td>
                            Comp Lab 18 max
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <td>Date/s of Event:</td>
            <td></td>

        </tr>
        <tr>
            <td>NB: Take note that IBTC Venues are only available between 07:15 in the morning until 15:45 in the afternoons on weekdays. Any afterhours activities must be arranged and approved by IBTC management in advance. </td>
            <td></td>

        </tr>
        <tr>
            <td>Name of Event Coordinator:</td>
            <td> <asp:TextBox ID="TextBox5"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Cost Centre:</td>
            <td> <asp:TextBox ID="TextBox6"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Functional Area:</td>
            <td> <asp:TextBox ID="TextBox7"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>G/L Account:</td>
            <td> <asp:TextBox ID="TextBox8"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Tel no:</td>
            <td> <asp:TextBox ID="TextBox9"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Fax no:</td>
            <td> <asp:TextBox ID="TextBox10"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Cell no:</td>
            <td> <asp:TextBox ID="TextBox11"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Email Address:</td>
            <td> <asp:TextBox ID="TextBox12"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Status:</td>
            <td> <asp:DropDownList ID="ddl"  runat="server">
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Approved</asp:ListItem>
                </asp:DropDownList></td>

        </tr>
        <tr>
            <td>Date:</td>
            <td> <asp:TextBox ID="TextBox14" ToolTip="Start Date"  runat="server"></asp:TextBox><asp:TextBox ID="TextBox13" ToolTip="End Date"  runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td></td>
            <td></td>

        </tr>
    </table>
    </div>
    </form>
</body>
</html>
