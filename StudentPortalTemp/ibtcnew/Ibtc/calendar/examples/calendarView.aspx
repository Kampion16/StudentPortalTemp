<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calendarView.aspx.cs" Inherits="calendar_examples_calendarView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset='utf-8' />
    <link href='../packages/core/main.css' rel='stylesheet' />
    <link href='../packages/daygrid/main.css' rel='stylesheet' />
    <link href='../packages/timegrid/main.css' rel='stylesheet' />
    <link href='../packages/list/main.css' rel='stylesheet' />
    <script src='../packages/core/main.js'></script>
    <script src='../packages/interaction/main.js'></script>
    <script src='../packages/daygrid/main.js'></script>
    <script src='../packages/timegrid/main.js'></script>
    <script src='../packages/list/main.js'></script>
    <script src="js/jquery-1.7.2.min.js"></script>

    <!--<link href="../packages/modal.css" rel="stylesheet" />-->
    <script>
        function modaal() {
            var span = document.getElementsByClassName("close")[0];
            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }
            var modal = document.getElementById("myModal");
            modal.style.display = "block";
        }

    </script>
    <style type="text/css">
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        .ModalHeadBorder {
            border-bottom: 2px solid #97e5e0;
        }

        .Modalheaders {
            font-size: 18px;
        }



        .td2 {
            text-align: right;
        }

        /* Modal Content/Box */
        .modal-content {
            background-color: #fefefe;
            margin: 15% auto; /* 15% from the top and centered */
            padding: 20px;
            border: 1px solid #888;
            width: 30%; /* Could be more or less, depending on screen size */
            height: 70%;
        }

        /* The Close Button */
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        .button {
            background-color: #2C3E50;
            /*border-radius: 1px;*/
            border-color: #2C3E50;
            color: white;
            padding: .5em;
            text-decoration: none;
            width: 100px;
            margin-top: 0px;
            text-align: center;
            margin-left: 10px;
        }

            .button:focus,
            .button:hover {
                background-color: black;
                color: White;
                cursor: pointer;
            }

        .calendarA {
            text-decoration: none;
            color: white;
        }
    </style>

    <script>

        var DBEvents = [

        ];


        function formateDateTime(date) {
            // PushEvnts();
            var d = new Date(date),
                year = d.getFullYear(),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
            hours = d.getHours(),
            minutes = d.getMinutes(),
            seconds = d.getSeconds();


            if (month.length < 2)
                month = '0' + month;

            if (day.length < 2)
                day = '0' + day;

            if (hours.toString().length < 2)
                hours = '0' + hours;

            if (minutes.toString().length < 2)
                minutes = '0' + minutes;

            if (seconds.toString().length < 2)
                seconds = '0' + seconds;


            return [year, month, day].join('-') + 'T' + hours + ':' + minutes + ':' + seconds;
        }

        $.ajax({
            type: "POST",
            url: "http://www.dwa.gov.za/ibtcnew/ibtc/ibtcwebservice.asmx/LoadCourses",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //dataType: 'xml',   
            success: function (data) {
                $.each(data, function (i, v) {
                    $.each(v, function (k) {
                        DBEvents.push({
                            title: String(v[k]._venue),
                            start: formateDateTime(v[k]._SdateTime),
                            end: formateDateTime(v[k]._EdateTime),
                            clientName: String(v[k]._name),
                            clientSurname: String(v[k]._surname),
                            clientEmail: String(v[k]._email),
                            clientCell: String(v[k]._Cell),
                            clientTell: String(v[k]._Tell),
                            clientNameSurnameID: String(v[k]._NameSurnameID),
                            eventDesc: String(v[k]._eventDesc),
                            NoAttendes: String(v[k]._noAttendees),
                            ClientID: String(v[k]._ClientID),
                            VenueID: String(v[k]._VenueID),
                            CourseID: String(v[k].CourseID),
                            EventName: String(v[k]._eventName)

                        });
                        // start: String(formateDateTime(v[0]._SdateTime)),
                    });
                    //    alert(JSON.stringify(DBEvents));
                }

            )
                GenrateCalendar();
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    alert(error);
                }
                else if (jqXHR.status == 404) {
                    alert('Requested page not found. [404]');
                } else if (jqXHR.status == 500) {
                    alert('Internal Server Error [500].');
                } else if (exception === 'parsererror') {
                    alert('Requested JSON parse failed.');
                } else if (exception === 'timeout') {
                    alert('Time out error.');
                } else if (exception === 'abort') {
                    alert('Ajax request aborted.');
                } else {
                    alert('Uncaught Error.\n' + jqXHR.responseText);
                }
            }
        })
        function GenrateCalendar() {
            var todaysDate = new Date();
            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {

                plugins: ['interaction', 'dayGrid', 'timeGrid', 'list'],
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
                },
                eventLimit: true,
                defaultDate: formateDateTime(todaysDate),
                navLinks: true, // can click day/week names to navigate views
                businessHours: true, // display business hours
                editable: true,
                
                events: DBEvents,
                eventClick: function (calEvent, jsEvent, view) {
                    modaal();

                    //Venue Details:
                    $('#myModal #eventTitle').text(calEvent.event.title);
                    $('#myModal #startDate').text(formateDateTime(calEvent.event.start));
                    $('#myModal #EndDate').text(formateDateTime(calEvent.event.end));
                    $('#myModal #VenueID').text(calEvent.event.extendedProps.VenueID);
                    document.getElementById('<%=Hidden1.ClientID %>').value = calEvent.event.extendedProps.VenueID;
                    // $('#myModal #').text(calEvent.event.extendedProps.VenueID);

                    //Event Details
                    $('#myModal #VenueTitle').text(calEvent.event.extendedProps.EventName);
                    $('#myModal #No').text(calEvent.event.extendedProps.NoAttendes);
                    //  $('#myModal #VstartDate').text(formateDateTime(calEvent.event.start));
                    //  $('#myModal #VEndDate').text(formateDateTime(calEvent.event.end));
                    $('#myModal #Desc').text(calEvent.event.extendedProps.eventDesc);


                    //Client Details:
                    $('#myModal #clientCell').text(calEvent.event.extendedProps.clientCell);
                    $('#myModal #clientNAme').text(calEvent.event.extendedProps.clientName);
                    $('#myModal #clientsurname').text(calEvent.event.extendedProps.clientSurname);
                    $('#myModal #clientMail').text(calEvent.event.extendedProps.clientEmail);
                    $('#myModal #clientTell').text(calEvent.event.extendedProps.clientTell);
                    $('#myModal #ClientID').text(calEvent.event.extendedProps.ClientID);

                }

            });

            calendar.render();

        }


        function GetConfirmation() {
            var reply = confirm("Ary you sure you want to delete this?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }

</script>
    <style>
        body {
            margin: 40px 10px;
            padding: 0;
            font-family: Arial, Helvetica Neue, Helvetica, sans-serif;
            font-size: 14px;
        }

        #calendar {
            max-width: 700px;
            margin: 0 auto;
        }
        ul#menu li {
  display:inline;
}
    </style>
</head>
<body>


    <form runat="server">
        <table style="width: 1024px; margin-left: 120px; margin-top: -30px;">
            <tr>
                <td>
                    <!-- #include file=/Includes/headerH.htm -->
                </td>
            </tr>
            <tr>

                <td>
                    <table style="width:1024px; margin-left:235px">
                        <tr>
                            <td>
                                <ul id="menu">
                                    <li> <a class="button"  href="http://webtest.dwa.gov.za/ibtcnew/ibtc/bookVenueT.aspx">Book Venue</a></li>
                                    <li> <a class="button"  href="http://webtest.dwa.gov.za/ibtcnew/ibtc/AddVenueType.aspx">Add Venue</a></li>
                                    <li> <a class="button"  href="http://webtest.dwa.gov.za/ibtcnew/ibtc/AddEventType.aspx">Add Event</a></li>
                                  <li> <a class="button"  href="http://webtest.dwa.gov.za/ibtcnew/ibtc/ExportWeek.aspx">Export Week</a></li>
                                </ul>                                                               
                            </td>
                        </tr>
                    </table>

                </td>
              

            </tr>
        </table>
        <div id='calendar'></div>
        <div id="myModal" class="modal">

            <!-- Modal content -->
            <div class="modal-content">
                <span class="close">&times;</span>
                <table width="100%">
                    <tr>
                        <td class="ModalHeadBorder">
                            <span class="Modalheaders"><b>Venue Details:</b></span>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>VenueID:    
                        </td>
                        <td class="td2">
                            <asp:Label runat="server" ID="VenueID"></asp:Label>
                            <asp:HiddenField ID="Hidden1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Name:    
                        </td>
                        <td class="td2">
                            <span id="eventTitle"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>Start Date:    
                        </td>
                        <td class="td2">
                            <span id="startDate"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>End Date:    
                        </td>
                        <td class="td2">
                            <span id="EndDate"></span>

                        </td>
                    </tr>

                </table>
                <table width="100%">
                    <tr>
                        <td class="ModalHeadBorder">
                            <span class="Modalheaders"><b>Event Details:</b></span>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>Name:    
                        </td>
                        <td class="td2">
                            <span id="VenueTitle"></span>
                        </td>
                    </tr>

                    <tr>
                        <td>Decription:    
                        </td>
                        <td class="td2">
                            <span id="Desc"></span>

                        </td>
                    </tr>
                    <tr>
                        <td>No of Attendees:    
                        </td>
                        <td class="td2">
                            <span id="No"></span>

                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="ModalHeadBorder">
                            <span class="Modalheaders"><b>Client Details:</b></span>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>Client ID:    
                        </td>
                        <td class="td2">
                            <span runat="server" id="ClientID"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>Name:    
                        </td>
                        <td class="td2">
                            <span id="clientNAme"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>surname:    
                        </td>
                        <td class="td2">
                            <span id="clientsurname"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>Tell No.:    
                        </td>
                        <td class="td2">
                            <span id="clientTell"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>Cell No.:    
                        </td>
                        <td class="td2">
                            <span id="clientCell"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>Email address.:   
                        </td>
                        <td class="td2">
                            <span id="clientMail"></span>
                        </td>
                    </tr>
                </table>
                <table style="margin: 0 auto;">
                    <tr>
                        <td>
                            <div style="margin-left: 60px; width: 436px">
                                <table>
                                    <tr>
                                        <td>
                                            <div>
                                                <asp:Button ID="btnup" runat="server" OnClick="btnup_Click" Text="Update" CssClass="button" />
                                                <%--<a class="calendarA" href="Default - copy.aspx">Update</a>--%>
                                            </div>

                                        </td>
                                        <td>
                                            <div>
                                                <asp:Button ID="btndel" runat="server" OnCommand="btndel_Click" OnClientClick="return GetConfirmation()" Text="Delete" CssClass="button" />
                                                <%--<a class="calendarA" href="Default - copy.aspx">Delete</a>--%>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

        </div>
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
