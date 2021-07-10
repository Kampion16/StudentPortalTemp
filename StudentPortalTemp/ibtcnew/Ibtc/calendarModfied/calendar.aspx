<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="calendar_calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>CIWSP Events Calendar</title>
    <!--<meta content="IE=edge" http-equiv="X-UA-Compatible">-->
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js">
        </script>
    <link href="stylesheet.css" rel="stylesheet"/>
  <%-- <script type="text/javascript">
       function getData() {
          
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "IBTCWebService.asmx/LoadCourses",
               data: "{}",
               dataType: "json",
               success: function (data) {
                   $.each(data.d, function (key, value) {
                       $("#ddl11").append($("<option></option>").val(value._VenueID).html(value._venue + "- " + value._name + " '" + value._surname + "'"));

                   });

               },
               error: function (result) {
                        //alert("Error");
               }
           });
           //alert("hh");
          
       };
       getData();
    </script>--%>
    
    <style>
        html {
            box-sizing: border-box;
            font-size: 10px;
        }

        body {
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
            color: #333;
            font-size: 1.6rem;
          <link href="stylesheet.css" rel="stylesheet">
            -webkit-font-smoothing: antialiased;
        }

        .logo {
            margin: 1.6rem auto;
            text-align: center;
        }

        a,
        a:visited {
            color: #0A9297;
        }

        footer {
            text-align: center;
            width: 244px;
        }

        h1 {
            text-align: center;
            width: 261px;
            height: 40px;
            margin-top: 4px;
            margin-bottom: 0px;
        }

        .container {
            width: 96%;
            max-width: 42rem;
            text-align: center;
            margin-left: 0px;
        }

        .demo-picked {
            font-size: 1.2rem;
            text-align: center;
        }

            .demo-picked span {
                font-weight: bold;
            }

        .auto-style1 {
            width: 271px;
        }

        .auto-style3 {
            width: 54px;
            text-align: right;
        }

        .auto-style4 {
            width: 138px;
        }
    </style>
</head>
<body>
    <label id="DateDet"></label>
    <form id="form1" runat="server">s
     <table style="width:580px;  ">
        <tr>            
            <td valign="Top">
                  
                <div class="container" >
                    <div id="v-cal" style="width: 730px;  height: 622px">
                        <div class="vcal-header">
                            <button class="vcal-btn" data-calendar-toggle="previous">
                                <svg height="24" version="1.1" viewbox="0 0 24 24" width="14" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M20,11V13H8L13.5,18.5L12.08,19.92L4.16,12L12.08,4.08L13.5,5.5L8,11H20Z"></path>
                                </svg>
                            </button>

                            <div class="vcal-header__label" data-calendar-label="month">
                                March 2017			
                            </div>


                            <button class="vcal-btn" data-calendar-toggle="next">
                                <svg height="24" version="1.1" viewbox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M4,11V13H16L10.5,18.5L11.92,19.92L19.84,12L11.92,4.08L10.5,5.5L16,11H4Z"></path>
                                </svg>
                            </button>
                        </div>
                        <div class="vcal-week" >
                            <span>Mon</span>
                            <span>Tue</span>
                            <span>Wed</span>
                            <span>Thu</span>
                            <span>Fri</span>
                            <span>Sat</span>
                            <span>Sun</span>
                        </div>
                        <table  class="vcal-body" data-calendar-area="month"></table>
                    </div>
                    <button id="loadscript">Load Script</button>
                   <!-- <p class="demo-picked">
                        Date picked:
		
                        <span data-calendar-label="picked"></span>
                    </p>-->

                </div>
            </td>
        </tr>
         
    </table>
      <%--  <table>
             <tr>
                 <td>
            <asp:DropDownList ID="ddl11" runat="server"></asp:DropDownList>
                 </td>
           
      </tr>
        </table>--%>
         
        <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
                <Services>
                    <asp:ServiceReference Path="~/IBTCWebService.asmx" />
                </Services>
            </asp:ScriptManager>--%>
    </form>
      <script src="script.js" type="text/javascript"></script>
    <script>

        window.addEventListener('load', function () {
            vanillaCalendar.init({
                disablePastDays: false
            }
            );

        })
       // getData();
        //function loadscript()
        //{
        //    var getTable = document.getElementsByClassName("vcal-date");
        //    var rows = getTable[1].
        //    alert(getTable);
        //}

	</script>
</body>
</html>
