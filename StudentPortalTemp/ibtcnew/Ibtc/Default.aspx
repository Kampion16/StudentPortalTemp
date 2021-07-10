<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"  enableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet/Default.css" />
    <%--<link rel="stylesheet" type="text/css" href="StyleSheet/Scheduling.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js">
        </script>
    <script type="text/javascript">
        
        function getData()
        {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "http://webtest.dwa.gov.za/ibtcnew/ibtc/ibtcwebservice.asmx/LoadCourses",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    $.each(data.d, function (key, value) {
                        $("#ddl11").append($("<option></option>").val(value._VenueID).html(value._venue + "- " + value._name + " '" + value._surname + "'"));

                    });
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }
        getData();
  
       // alert("what");
    </script>
    <title></title>
    
</head>
<body>
    <form  runat="server">
         <table style=" margin: 0 auto">
            <tr>
                <td>
                     <!-- #include file=~/Includes/headerH.htm -->
                </td>
            </tr>
        
            </table>
         <label id="DateDet"></label>
        <table>
            <tr>
                <td valign="top">
                      <table style="margin-left: 150px; margin-top:33px">
             <tr><td >
           <button type="button"  class="log-btn" >View Course Schedule</button><br /><br />                 
               </td></tr> 
            <tr><td>
               <button type="button" onclick="document.getElementById('id02').style.display='block'"  class="log-btn" data-toggle="modal" data-target="#myModal"  >Book Venue</button><br /><br />
               </td></tr>
           <tr><td >
           <button type="button" onclick="document.getElementById('id01').style.display='block'"  data-toggle="modal" data-target="#myModal" class="log-btn" >Upload Course</button><br /><br />
               </td></tr>
          
         
           <tr><td>
              
               <button type="button" class="log-btn" >View Venue Bookings</button><br /><br />
               </td></tr>
       </table>
                </td>
                <td>
                    <table style="width:100%; margin: 0 auto  ">
            <tr>
                <td>
                       <iframe scrolling="no" frameborder="0" height="580px" width="750px"   src="calendar/index.html"></iframe>
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
                  
  <table  align="center" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; background-color: #ffffff">
      <tr>
           <asp:DropDownList ID="ddl11" runat="server"></asp:DropDownList>
      </tr>        
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
