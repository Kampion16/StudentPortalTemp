<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadCourse.aspx.cs" Inherits="UploadCourse"  enableEventValidation="false"%>

<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
        <link rel="stylesheet" type="text/css" href="StyleSheet/BookVenuT.css" />  
<script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>

     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

  <link rel="stylesheet" href="/resources/demos/style.css" />

    <title>Schedule Course</title>
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
            text-align:left;
             width: 22px;
        }
        .spanRed {
        color:red
        }
    </style>
</head>

<body>
    <form id="form1" runat="server" >
        <table style=" margin: 0 auto;  width: 1024px">
            <tr>
                <td>
                     
                </td>
            </tr>

              <tr>
                <td>

                    <h1 style="text-align: center">Schedule an Event</h1>
                    <p style="text-align: center">Please provide details of the person responsible for the Event:</p>
                </td>
            </tr>
        
            </table>
   <table style="margin: 0 auto; border:solid 1px; width:600px">
          <tr>
              <td>
      <table style="margin: 0 auto;table-layout: fixed; background-color:white;width:600px">
          <tr>

              <td class="auto-style2"> Name:  <span class="spanRed">*</span>
              </td>
              <td class="auto-style1">
                <asp:TextBox runat="server" ID="clientName"  />                
              </td>
              <td class="alignleft">
                  <asp:RequiredFieldValidator ID="vname" runat="server" ControlToValidate="clientName" CssClass="validators"  ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>  
             <p></p>
                   </td>
          </tr>
           <tr>
               <td class="auto-style2">Surname:  <span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
                         <asp:TextBox runat="server" ID="clientsurname"  /><p></p>                         
              </td>
               <td class="alignleft">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validators" ControlToValidate="clientsurname" ErrorMessage="Surname cannot be empty"></asp:RequiredFieldValidator>  
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
           <tr>
               <td class="auto-style2"> Booking Start Date:  <span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
              <asp:TextBox  style="width: 150px;" runat="server"  ID="SDate" />
                  
              </td>
               <td class="alignleft">
                    <asp:Label ID="SdateLbl" runat="server" Visible="false" class="lblColor" ></asp:Label>  
                 <asp:RequiredFieldValidator ID="sdtlbl" runat="server" CssClass="validators" ErrorMessage="Start Date cannot be empty" ControlToValidate="SDate"></asp:RequiredFieldValidator>               
                    <p></p>
               </td>
          </tr>
           <tr>
               <td class="auto-style2">  Booking End Date:  <span class="spanRed">*</span>
               </td>
               <td class="auto-style1">
                <asp:TextBox  style="width: 150px;" runat="server"  width="270px" id="EDate"  />
                <p></p>
               </td>
               <td class="alignleft">
                   <asp:Label ID="EDateLbl" runat="server" class="lblColor" ></asp:Label>  
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validators" ErrorMessage="End Date cannot be empty" ControlToValidate="EDate"></asp:RequiredFieldValidator>   
               <p></p>
                    </td>
             
          </tr>
           <tr >
               <td class="auto-style2"> Select Event:<span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
                  <asp:DropDownList ID="DropDownList2" class="ddl" runat="server" AppendDataBoundItems="true">
                       <asp:ListItem Text="---------------------------------------------"></asp:ListItem>
                    
                       <%--<asp:ListItem Text="Computer Lab"></asp:ListItem>--%>
                  </asp:DropDownList><p></p>
              </td>
               <td class="alignleft">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validators"  ControlToValidate="DropDownList2" InitialValue="---------------------------------------------"  ErrorMessage="Please Select Event"></asp:RequiredFieldValidator>  
              <p></p>
                        </td>
 
          </tr>
           <tr>
               <td class="auto-style2"> Venue Type: <span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
                  <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" class="ddl">
                
                  </asp:DropDownList>
              </td>
               <td class="alignleft">
                     <asp:Label ID="userLbl" runat="server" class="lblColor" Visible="false" ></asp:Label>       
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="validators" ControlToValidate="DropDownList1" InitialValue=""  ErrorMessage="Please Select Venue"></asp:RequiredFieldValidator>  
               <p></p>
               </td>
          </tr>
          </table>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
                <Services>
                    <asp:ServiceReference Path="~/IBTCWebService.asmx" />
                </Services>
            </asp:ScriptManager>
          <table style="margin: 0 auto;width: 600px;text-align: center; table-layout: fixed; background-color:white">
              <tr>                 
              <td>
                   <p>Please provide details of the person that is booking the Event - Administrator:</p>
              </td>              
                 </tr>            
          </table>
            <table style="margin: 0 auto; width: 600px;table-layout: fixed; background-color:white">          
             <tr>
               <td class="auto-style2">Bookmaker Name: <span class="spanRed">*</span>
               </td>
              <td class="auto-style1">
                  <asp:TextBox runat="server" ID="Bookiename"  /><p></p>                                
              </td>
                 <td class="alignleft">
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="validators" ControlToValidate="Bookiename" ErrorMessage="Name cannot be empty"></asp:RequiredFieldValidator>
                 <p></p>
                      </td>
          </tr>
          <tr>
              <td class="auto-style2">Bookmaker Surname:  <span class="spanRed">*</span>  
              </td>
              <td class="auto-style1">
                <asp:TextBox runat="server" ID="Bookiesurname"  /><p></p>                                
              </td>
              <td  class="alignleft">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="validators" ControlToValidate="Bookiesurname" ErrorMessage="Surname cannot be empty"></asp:RequiredFieldValidator>  
              </td>
          </tr>
            <tr>
              <td class="auto-style2"> Bookmaker Email:    <span class="spanRed">*</span>
              </td>
               
              <td class="auto-style1">
                   <asp:TextBox runat="server" ID="Bookieemail"  /><p></p>                                
              </td>
                <td class="alignleft">
                 <asp:Label ID="BookieemailLbl" runat="server" class="lblColor" Visible="false" ></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Bookieemail" CssClass="validators"  ErrorMessage="Email cannot be empty"></asp:RequiredFieldValidator>  
                    </td>
          </tr>
          <tr>
              <td class="auto-style2">Bookmaker Fax No.:    <span class="spanRed">*</span>
              </td>
              <td class="auto-style1">
                        <asp:TextBox runat="server" ID="BookieFax"  /><p></p>                                
              </td>
              <td class="alignleft">
                   <asp:Label ID="BookieFaxLbl" runat="server" class="lblColor" ></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="validators" runat="server" ControlToValidate="BookieFax" ErrorMessage="Bookie Fax No. cannot be empty"></asp:RequiredFieldValidator>    
              </td>
          </tr>
           <tr>
              <td class="auto-style2"> Bookmaker Tell No.:    <span class="spanRed">*</span>
              </td>
              <td class="auto-style1">
                <asp:TextBox runat="server" ID="Bookiecell"   /><p></p>                                
              </td>
               <td class="alignleft">
                    <asp:Label ID="BookiecellLbl" runat="server" class="lblColor" ></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="validators" ControlToValidate="Bookiecell" ErrorMessage="Bookie Tell No. cannot be empty"></asp:RequiredFieldValidator> 
               </td>
          </tr>
      </table>
     </td>
              </tr>
       </table>
      
    
          <table style="margin: 0 auto;">
              <tr>
                    <td>
                         <div class="button" >
           <a   href="Default - copy.aspx">Cancel</a></div>
  
              </td>
              <td>
                   <div class="button" >
        <asp:Button ID="btn5" runat="server" style="background-color:transparent;border:none; color:white;font-size:16px;" OnClick="btn5_Click"  Text="Submit"></asp:Button></div>
              </td>
                     </tr>
          </table>      

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
  
         <%--  <tr>
               <td class="alignleft">
                   Email: 
               </td>
              <td>
                 
                    <asp:TextBox runat="server" ID="clientemail"   /><p></p>             
              </td>
               <td class="alignleft">
                    <asp:Label ID="clientemailLbl" Visible="false" runat="server" class="lblColor" ></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="clientemail"  ErrorMessage="Email cannot be empty"></asp:RequiredFieldValidator>  
               </td>
          </tr>--%>
         <%--  <tr>
               <td class="alignleft">
                   Cell No.:  
               </td>
              <td>
                   <asp:TextBox runat="server" ID="Cell"   /><p></p>                
              </td>
               <td class="alignleft">
                  <asp:Label ID="lblCell" Visible="false" runat="server" class="lblColor" ></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Cell" Visible="true"  ErrorMessage="Cell No. cannot be empty"></asp:RequiredFieldValidator>  
               </td>
          </tr>--%>
          <%-- <tr>
               <td class="alignleft">
                   Fax No.:  
               </td>
              <td>
                   <asp:TextBox runat="server" ID="Fax"  /><p></p>                
              </td>
               <td class="alignleft">
                   <asp:Label ID="lblFax" runat="server" class="lblColor" Visible="false"  ></asp:Label>        
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Fax"  ErrorMessage="Fax No. cannot be empty"></asp:RequiredFieldValidator>   
               </td>
          </tr>--%>
         <%--  <tr>
               <td class="alignleft">
                    Tell No.:  
               </td>
              <td>
                   <asp:TextBox runat="server" ID="Tell"  /><p></p>                
              </td>
               <td class="alignleft">
                       <asp:Label ID="lblTel" runat="server" class="lblColor" Visible="false" ></asp:Label>       
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Tell"  ErrorMessage="Tell No. cannot be empty"></asp:RequiredFieldValidator>  
               </td>
          </tr>--%>