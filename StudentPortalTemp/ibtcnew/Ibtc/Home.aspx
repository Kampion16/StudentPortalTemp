<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
    <%--<link rel="stylesheet" type="text/css" href="StyleSheet/Scheduling.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js">
        </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#b1").click(function (event) {
                var flag = 'T';// to check the status and display final message box
                // Add the pointer image to message boxes ///
                $("#d1,#d2").html("<img class='pointer' src='pointer2.gif' />");

                if ($("#fname").val().length < 1) { // if there is no entry for first name
                    $("#d1").append('Enter First name'); // Add message to tooltip box
                    $("#d1").show(); // display the tooltip
                    flag = 'F';	// set the flag to false 
                } else {
                    /// user has enterd its first name 
                    $("#d1").hide(); // hide the tooltip box
                }

                ///start checking second text box //
                if ($("#lname").val().length < 1) {
                    $("#d2").append('Enter Last name');
                    $("#d2").show();
                    flag = 'F';
                } else {
                    $("#d2").hide();
                }

                // end of checking two text boxes //

                if (flag == 'T') {
                    $("#msg").html('Welcome ' + $("#fname").val() + ' ' + $("#lname").val());
                    $("#msg").show();
                }

                // hide all the message boxes after time interval ///
                setTimeout(function () { $("#d1,#d2,#msg").fadeOut('slow'); }, 3000);

            });
        });
        //function getData()
        //{
        //    $.ajax({
        //        type: "POST",
        //        contentType: "application/json; charset=utf-8",
        //        url: "IBTCWebService.asmx/LoadCourses",
        //        data: "{}",
        //        dataType: "json",
        //        success: function (data) {
        //            $.each(data.d, function (key, value) {
        //                $("#ddl11").append($("<option></option>").val(value._VenueID).html(value._venue + "- " + value._name + " '" + value._surname + "'"));

        //            });
        //        },
        //        error: function (result) {
        //            alert("Error");
        //        }
        //    });
        //}
        //getData();

        // alert("what");
    </script>
    <title></title>
    
</head>
<body>
    <form id="Form1"  runat="server">
         <table width="980px" style=" margin: 0 auto">
            <tr>
                <td>
                     <!-- #include file=~/Includes/headerH.htm -->
                </td>
            </tr>
        
            </table>
         <label id="DateDet"></label>
        <table style="width:1024px; margin-left: 99px;"> 
           
            <tr>
                <td>
                     <table align="center">
             <tr>
                 <td >
           <asp:Button ID="vwCourse" runat="server" Text="View Course Schedule" class="log-btn" OnClick="vwCourse_Click" /><br /><br />                 
               </td>
                 <td>
               <asp:Button ID="Button1"  OnClientClick="window.open('BookVenueT.aspx')" runat="server"  class="log-btn"  Text="Book Venue"  /><br /><br />
               </td>
                 <td >
           <asp:Button ID="Button2"  OnClientClick="window.open('uploadcourse.aspx')"  runat="server"  class="log-btn" Text="Upload Course" /><br /><br />
               </td>
                 <td>
              <asp:Button ID="vwVenue" class="log-btn"  runat="server" Text="View Venue Bookings" OnClick="vwVenue_Click" /><br /><br />    
               
               </td>
             </tr> 
           
       </table>
                </td>
            </tr>
             <tr>
                <td>
                    <div style="font-size:20px;"><b><asp:Label ID="vnb" runat="server" Text="Venue Bokings"></asp:Label></b></div>
                </td>
            </tr>
            <tr>
               
                <td>
                    <table style="width:100%; margin: 0 auto  ">
            <tr>
                <td>
                      <asp:GridView ID="GridView1" CellPadding="2" AllowPaging="True" PageSize="7" ForeColor="#333333" Width="980px" style="margin-left:35px;"  EmptyDataText="No Record Found" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" EnableModelValidation="True">
            <RowStyle BackColor="#dadfbc" Height="30px"  Wrap="false" />
               <HeaderStyle Width="10%" />
             <Columns>
                <asp:BoundField DataField="_venue" HeaderText="Venue Name" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_venue" />
                <asp:BoundField DataField="_SdateTime" HeaderText="Start Date" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_SdateTime" />
                <asp:BoundField DataField="_EdateTime" HeaderText="End Date" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_EdateTime" />
                <asp:BoundField DataField="_name" HeaderStyle-Width="15%" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderText="Name" SortExpression="_name" />
                <asp:BoundField DataField="_surname" HeaderStyle-Width="15%" HeaderStyle-Height="35px" HeaderText="Surname" HeaderStyle-Wrap="false" SortExpression="_surname" />
                <asp:BoundField DataField="_email" HeaderText="Email" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_email" />
                 <asp:BoundField DataField="_Cell" HeaderText="Cell Number" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" SortExpression="_Cell" />
                 <asp:BoundField DataField="_Tell" HeaderText="Work Number" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" SortExpression="_Tell" />
            </Columns>
             <FooterStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#838F57" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" Wrap="False" />
        <AlternatingRowStyle BackColor="White" Height="40px" Wrap="True" />
        </asp:GridView>

        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="LoadCourses" TypeName="IBTCWebService"></asp:ObjectDataSource>

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Visible="False" DataSourceID="ObjectDataSource1" style="margin-left:35px;"  CellPadding="2" AllowPaging="True" PageSize="4" ForeColor="#333333" Width="980px"  EmptyDataText="No Record Found"  EnableModelValidation="True" OnRowEditing="GridView2_RowEditing" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowUpdating="GridView2_RowUpdating" DataKeyNames="CourseID">
             <RowStyle BackColor="#dadfbc" Height="30px"  Wrap="true" />
               <HeaderStyle Width="10%" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField HeaderText="ID"  SortExpression="CourseID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" style="width:20px" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Height="35px" Wrap="False" />
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="courseName"  SortExpression="courseName">

                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" style="width:150px" runat="server" SelectedValue='<%# Bind("courseName") %>'  >
                            <asp:ListItem >----Select Course----</asp:ListItem>
                            <asp:ListItem >Safety,Health and Environment Business Relations</asp:ListItem>
                            <asp:ListItem >International Computer&#39;s Driver&#39;s License</asp:ListItem>
                            <asp:ListItem >International Programmes</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1" InitialValue="----Select Course----" ErrorMessage="Course name is Required" Text="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("courseName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Height="35px" Width="10%" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Attendees" HeaderStyle-Width="15%" SortExpression="_noAttendees">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" style="width:70px" runat="server" Text='<%# Bind("_noAttendees") %>'></asp:TextBox>
                        <asp:Label runat="server" ID="attendeesLbl2" style="margin-left:35px" ForeColor="Red" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"  ErrorMessage="Attendees is Required" Text="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("_noAttendees") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Height="35px" Width="15%" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="15%" SortExpression="_SdateTime">
                    <EditItemTemplate>
                        <asp:TextBox ID="_SdateTime"  style="width:150px" runat="server" Text='<%# Bind("_SdateTime") %>'></asp:TextBox>  
                        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>                     
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_SdateTime"  ErrorMessage="Start Date is Required" Text="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("_SdateTime") %>'></asp:Label>
                         
                    </ItemTemplate>
                    <HeaderStyle Height="35px" Width="15%" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" HeaderStyle-Width="15%" SortExpression="_EdateTime">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3"  style="width:150px" runat="server" Text='<%# Bind("_EdateTime") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox3"  ErrorMessage="End Date is Required" Text="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("_EdateTime") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="15%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="15%" SortExpression="_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1"  style="width:100px" runat="server" Text='<%# Bind("_name") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1" ErrorMessage="Name is required" Text="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("_name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Height="35px" Width="15%" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Surname" HeaderStyle-Width="15%" SortExpression="_surname">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4"  style="width:100px" runat="server" Text='<%# Bind("_surname") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox1" ErrorMessage="Surname is required" Text="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("_surname") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Height="35px" Width="15%" Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="_venue" HeaderText="Venue" SortExpression="_venue" />
                <asp:BoundField DataField="_email" HeaderText="Email" SortExpression="_email" />
                <asp:BoundField DataField="_Cell" HeaderText="Cell Number" SortExpression="_Cell" />
                <asp:BoundField DataField="_Tell" HeaderText="Work Number" SortExpression="_Tell" />
            </Columns>
             <FooterStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#838F57" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" Wrap="False" />
        <AlternatingRowStyle BackColor="White" Height="40px" Wrap="True" />
        </asp:GridView>
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="validationsummary" ForeColor="Red" runat="server" />
                    <asp:Label runat="server" ID="attendeesLbl" style="margin-left:35px" ForeColor="Red" Visible="false"></asp:Label>
                     <asp:Label runat="server" ID="SDateLbl" style="margin-left:35px" ForeColor="Red" Visible="false"></asp:Label>
                     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="LoadVenues" TypeName="IBTCWebService" UpdateMethod="UpdateCourse">
                         <UpdateParameters>
                             <asp:Parameter Name="CourseID" Type="Int32" />
                             <asp:Parameter Name="courseName" Type="String" />
                             <asp:Parameter Name="_noAttendees" Type="String" />
                             <asp:Parameter Name="_SdateTime" Type="DateTime" />
                             <asp:Parameter Name="_EdateTime" Type="DateTime" />
                             
                             <asp:Parameter Name="_name" Type="String" />
                             <asp:Parameter Name="_surname" Type="String" />
                             <asp:Parameter Name="_Cell" Type="Int32" />
                             <asp:Parameter Name="_Tell" Type="Int32" />
                             <asp:Parameter Name="_email" Type="String" />
                             <asp:Parameter Name="_venue" Type="String" />
                             
                         </UpdateParameters>
                      </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
                  
  <table  align="center" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; background-color: #ffffff">
     <%-- <tr>
           <asp:DropDownList ID="ddl11" runat="server"></asp:DropDownList>
      </tr>  --%>      
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
