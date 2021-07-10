<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateVenue.aspx.cs" Inherits="UpdateCourse" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet/BookVenuT.css" />
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <link rel="stylesheet" href="/resources/demos/style.css" />

    <title>Update Venue</title>
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
            $("#Edate").datepicker(
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

                        $('#Edate').val(datetext);
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
        <table style="margin: 0 auto; width:1024px">
            <tr>
                <td>
                    <!-- #include file=/Includes/headerH.htm -->
                </td>
            </tr>
            <tr>
                <td>
                    <h1 style="text-align: center">Update Venue Details</h1>
                    <p style="text-align: center">Please provide details of the person responsible for the Course:</p>

                </td>
            </tr>
        </table>

        <table style="margin: 0 auto; border: solid 1px; width: 600px">
            <tr>
                <td>
                    <table style="margin: 0 auto; table-layout: fixed; background-color: white; width: 600px">
                        <tr>

                            <td class="auto-style2">Name:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="clientName" /><p></p>

                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="vname" runat="server" ControlToValidate="clientName" ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Surname:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="clientsurname" /><p></p>
                                <asp:TextBox runat="server" ID="AjaxSurnameValue" Visible="false" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="clientsurname" ErrorMessage="Surname cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style2">Email: 
                            </td>
                            <td class="auto-style1">

                                <asp:TextBox runat="server" ID="clientemail" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="clientemailLbl" Visible="false" runat="server" class="lblColor"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="clientemail" ErrorMessage="Email cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Cell No.:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Cell" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="lblCell" Visible="false" runat="server" class="lblColor"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Cell" Visible="true" ErrorMessage="Cell No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Fax No.:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Fax" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="lblFax" runat="server" class="lblColor" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Fax" ErrorMessage="Fax No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Tell No.:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Tell" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="lblTel" runat="server" class="lblColor" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Tell" ErrorMessage="Tell No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style2">Booking Start Date:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" Style="width: 150px;" ID="SDate" />

                            </td>
                            <td class="alignleft">
                                <asp:Label ID="SdateLbl" runat="server" class="lblColor" ></asp:Label>  
                 
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Booking End Date:  
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" Style="width: 150px;" ID="Edate" />
                                <p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="EDateLbl" runat="server" class="lblColor" ></asp:Label>  
                 
                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">Select Venue:
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DropDownList2" class="ddl" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="---------------------------------------------"></asp:ListItem>

                                    <%--<asp:ListItem Text="Computer Lab"></asp:ListItem>--%>
                                </asp:DropDownList><p></p>
                            </td>
                             <td class="alignleft">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="validators"  ControlToValidate="DropDownList2" InitialValue="---------------------------------------------"  ErrorMessage="Please Select Venue"></asp:RequiredFieldValidator>  
              <p></p>
                        </td>
                        </tr>
                          <tr>
               <td class="auto-style2">  Max No. of Attendees:<span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
                   <asp:TextBox runat="server" ID="attendees"  /><p></p>                
              </td>
               <td class="alignleft">
                       <asp:Label ID="Label1" runat="server" class="lblColor" Visible="false" ></asp:Label>       
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="validators" ControlToValidate="attendees"  ErrorMessage="Max No. cannot be empty"></asp:RequiredFieldValidator>  
             <p></p>
                     </td>
          </tr>
                          <tr >
               <td class="auto-style2"> Select Event:<span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
                  <asp:DropDownList ID="DropDownList1" class="ddl" runat="server" AppendDataBoundItems="true">
                       <asp:ListItem Text="---------------------------------------------"></asp:ListItem>
                    
                       <%--<asp:ListItem Text="Computer Lab"></asp:ListItem>--%>
                  </asp:DropDownList><p></p>
              </td>
               <td class="alignleft">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" CssClass="validators"  ControlToValidate="DropDownList1" InitialValue="---------------------------------------------"  ErrorMessage="Please Select Event"></asp:RequiredFieldValidator>  
              <p></p>
                        </td>
 
          </tr>
                        <%--  <tr>
               <td>
                   Venue Type: 
               </td>
              <td>
                  <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" class="ddl">
                 <asp:ListItem Value="0" Text="-------------------------------------------------"></asp:ListItem>
                  </asp:DropDownList>
              </td>
               <td>
                        <asp:Label ID="userLbl" runat="server" class="lblColor" Visible="false" ></asp:Label>       
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="attendees"  ErrorMessage="Max No. cannot be empty"></asp:RequiredFieldValidator>  
               
               </td>
          </tr>--%>
                    </table>
                   <table style="margin: 0 auto; width: 600px; text-align: center; background-color: white">
                        <tr>
                            <td>
                                <p>Please provide details of the person that is Modifying the Course - Administrator:</p>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validators" runat="server" ControlToValidate="Bookiename" ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>
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
                            <td class="auto-style2">Bookmaker Fax No.:    
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="BookieFax" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="BookieFaxLbl" runat="server" class="lblColor"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="validators" ControlToValidate="BookieFax" ErrorMessage="Bookie Fax No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Bookmaker Tell No.:    
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox runat="server" ID="Bookiecell" /><p></p>
                            </td>
                            <td class="alignleft">
                                <asp:Label ID="BookiecellLbl" runat="server" class="lblColor"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="validators" ControlToValidate="Bookiecell" ErrorMessage="Bookie Tell No. cannot be empty"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                <Services>
                    <asp:ServiceReference Path="~/IBTCWebService.asmx" />
                </Services>
            </asp:ScriptManager>


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
