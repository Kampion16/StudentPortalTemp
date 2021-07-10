<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportWeek.aspx.cs" Inherits="ExportWeek" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet/BookVenuT.css" />

    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <link rel="stylesheet" href="/resources/demos/style.css" />
    <title>Export Weekly Schedules</title>
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
        

    </script>

     <style type="text/css">
        .auto-style1 {
            width: 25px;          
        }
         .auto-style3 {
            width: 45px;          
        }
        .auto-style2 {
            text-align:left;
             width: 22px;
        }
        .spanRed {
        color:red
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  <table style="margin: 0 auto; width: 1024px">
            <tr>
                <td>
                    <!-- #include file=/Includes/headerH.htm -->
                </td>
            </tr>
      <tr>
                <td>

                    <h1 style="text-align: center">Export Weekly Schedule</h1>
                    <p style="text-align: center">Please select Start and End Date for which you would like to export Schedules:</p>
                </td>
            </tr>
      </table>
         <table style="margin: 0 auto; table-layout: fixed; border:solid 1px; background-color: white; width:600px">
            
            <tr>
               
                <td class="auto-style2">Start Date:  <span class="spanRed">*</span>
                </td>
                  <td class="auto-style1">
                    <asp:TextBox Style="width: 150px;" runat="server" ID="SDate" />

                </td>
                <td class="alignleft">
                    <asp:Label ID="SdateLbl" runat="server" class="lblColor"></asp:Label>
                    <asp:RequiredFieldValidator ID="sdtlbl" runat="server" CssClass="validators" ErrorMessage="Start Date cannot be empty" ControlToValidate="SDate"></asp:RequiredFieldValidator>
                    <p></p>
                </td>
            </tr>
               <tr>
               
                <td class="auto-style2">End Date:  <span class="spanRed">*</span>
                </td>
                 <td class="auto-style1">
                    <asp:TextBox Style="width: 150px;" runat="server" Width="270px" ID="EDate" />
                    <p></p>
                </td>
                <td class="alignleft">
                    <asp:Label ID="EDateLbl" runat="server" class="lblColor"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="End Date cannot be empty" CssClass="validators" ControlToValidate="EDate"></asp:RequiredFieldValidator>
                    <p></p>

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

        <table align="center" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; margin-top:25px; background-color: #ffffff">
            <tr>
                <td>
                    <!-- #include file=footer.htm -->

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
