<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVenueType.aspx.cs" Inherits="AddEventType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet/BookVenuT.css" />
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <link rel="stylesheet" href="/resources/demos/style.css" />

    <title>Add Venue</title>
    <script>
        $(function () {
            $("#SDate").datepicker(
                {
                    dateFormat: 'yy-mm-dd',
                    onSelect: function (datetext) {
                        var d = new Date(); // for now

                        var h = d.getHours();
                        h = (h < 10) ? ("0" + h) : h;

                        var m = d.getMinutes();
                        m = (m < 10) ? ("0" + m) : m;

                        var s = d.getSeconds();
                        s = (s < 10) ? ("0" + s) : s;

                        datetext = datetext + " " + h + ":" + m + ":" + s;

                        $('#SDate').val(datetext);
                    },
                });


        });
        $(function () {
            $("#EDate").datepicker(
                {
                    dateFormat: 'yy-mm-dd',
                    onSelect: function (datetext) {
                        var d = new Date(); // for now

                        var h = d.getHours();
                        h = (h < 10) ? ("0" + h) : h;

                        var m = d.getMinutes();
                        m = (m < 10) ? ("0" + m) : m;

                        var s = d.getSeconds();
                        s = (s < 10) ? ("0" + s) : s;

                        datetext = datetext + " " + h + ":" + m + ":" + s;

                        $('#EDate').val(datetext);
                    },
                }
                );
        });
        function Validation() {

            //Clients Info
            var CellLbl = document.getElementById("lblCell");
            var Cell = document.getElementById("");

            if (Cell.innerText == "") {
                alert("empty");
                CellLbl.innerHTML = "Cell No. cannot be empty."
            }

        }


    </script>
     <style type="text/css">
        .auto-style1 {
            width: 25px;
        }

        .auto-style3 {
            width: 45px;
        }

        .auto-style2 {
            text-align: left;
            width: 22px;
        }

        .spanRed {
            color: red;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <table style="width: 1024px; margin: 0 auto">
            <tr>
                <td>
                    <!-- #include file=/Includes/headerH.htm -->
                </td>
            </tr>
            <tr>
                <td>
                    <h1 style="text-align: center">Add Venue</h1>
                    <p style="text-align: center">Please provide details of the Venue:</p>
                </td>
            </tr>

        </table>
        <table style="margin: 0 auto; border: solid 1px; width: 600px">
            <tr>
                <td>

                    <table style="margin: 0 auto; table-layout: fixed; background-color: white; width: 600px">
                        <tr>

                            <td class="auto-style2">Venue Name:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="EventName" /><p></p>

                            </td>
                            <td class="alignleft">
                                  <asp:Label runat="server" ForeColor="Red" ID="EventLBL" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="vname" runat="server" ControlToValidate="EventName" CssClass="validators" ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>

                            <td class="auto-style2">Venue Description:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="EventDes" /><p></p>

                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validators" ControlToValidate="EventDes" ErrorMessage="Event description cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

                    <table style="margin: 0 auto; width: 600px; text-align: center; table-layout: fixed; background-color: white">
                        <tr>

                            <td>
                                <p>Please provide details of the person that is adding the Event - Administrator:</p>
                            </td>
                        </tr>
                    </table>
                   <table style="margin: 0 auto; width: 600px; table-layout: fixed; background-color: white">
                        <tr>
                            
                            <td class="auto-style2">Bookmaker Name: 
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Bookiename" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="validators" ControlToValidate="Bookiename" ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                           
                            <td class="auto-style2">Bookmaker Surname:    
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Bookiesurname" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="validators" ControlToValidate="Bookiesurname" ErrorMessage="Surname cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                          
                            <td class="auto-style2">Bookmaker Email:    
                            </td>

                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Bookieemail" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="BookieemailLbl" runat="server" class="lblColor" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="validators" ControlToValidate="Bookieemail" ErrorMessage="Email cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                           
                            <td class="auto-style2">Bookmaker Tell No.:    
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="BookieFax" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="BookieFaxLbl" runat="server" class="lblColor"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="validators" ControlToValidate="BookieFax" ErrorMessage="Bookie Tell No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            
                            <td class="auto-style2">Bookmaker Cell No.:    
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Bookiecell" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="BookiecellLbl" runat="server" class="lblColor"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="validators" ControlToValidate="Bookiecell" ErrorMessage="Bookie Cell No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>                             
                </td>
            </tr>
        </table>
           <table style="margin: 0 auto;">
            <tr>
                <td>
                    <div style="margin-left: 140px; width: 436px">
                        <table>
                            <tr>
                                <td>
                                    <div class="button" >
                                        <a href="http://webtest.dwa.gov.za/ibtcnew/ibtc/calendar/examples/calendarview.aspx">Cancel</a>
                                    </div>

                                </td>
                                <td>
                                    <div class="button" >
                                        <asp:Button ID="btn5" runat="server" Style="background-color: transparent; border: none; color: white; font-size: 16px;" OnClick="btn5_Click" Text="Submit"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
        </table>

                 
        <table align="center" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; background-color: #ffffff">
            <tr>
                <td>
                    <!-- #include file=footer.htm -->

                </td>
            </tr>
        </table>

    </form>

</body>
</html>
