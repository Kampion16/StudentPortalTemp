using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookVenueT : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    IBTCWebService retrieveVenues = new IBTCWebService();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IBTCWebService ibtc = new IBTCWebService();
            List<VenueTypes> Ea = ibtc.LoadVenueTypes();
            DropDownList3.DataSource = Ea;
            DropDownList3.DataTextField = "VenueName";
            DropDownList3.DataValueField = "VenueTypeIDName";
            Session["username"] = clientName.Text;
            //List<EventsAttr> Ea = retrieveVenues.LoadEvents();
            //DropDownList2.DataTextField = "EventName";
            //DropDownList2.DataValueField = "EventIDName";
            //DropDownList2.DataSource = Ea;
            //DropDownList2.DataBind();
            List<EventsAttr> ae = retrieveVenues.LoadEvents();
            DropDownList2.DataTextField = "EventName";
            DropDownList2.DataValueField = "EventIDName";
            DropDownList2.DataSource = ae;
            DropDownList2.DataBind();
            DropDownList3.DataBind();    
        }
        
    }
    protected void btn5_Click(object sender, EventArgs e)
    {

        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        clientemailLbl.Text = "";
        lblCell.Text = "";
        lblFax.Text = "";
        lblTel.Text = "";
        BookieemailLbl.Text = "";
        BookieFaxLbl.Text = "";
        BookiecellLbl.Text = "";
        EDate.Visible = true;
        //Client Number validation
        Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        Match match = regex.Match(Cell.Text.Trim());
        Match matchFax = regex.Match(Fax.Text.Trim());
        Match matchTell = regex.Match(Tell.Text.Trim());
        bool SDateEdate = false;

        string expression = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
        int compareDate = DateTime.Compare(Edate1, Sdate1);

        if (Sdate1 < DateTime.Now)
        {
            SdateLbl.Text = "Booking Start Date cannot be a Past date";
            SDateEdate = true;
            string StartDte = SDate.Text;
            SDate.Text = StartDte;
            string EndtDte = EDate.Text;
            EDate.Text = EndtDte;
        }
        else  if (compareDate < 0 )
        {
             EDateLbl.Text = "Booking End Date must be greater than Booking Start Date";
                SDateEdate = true;
                string StartDte = SDate.Text;
                SDate.Text = StartDte;
                string EndtDte = EDate.Text;
                EDate.Text = EndtDte;
            
          
        }
        else
        {
            EDateLbl.Text = "";
            SDateEdate = false;
        }
        Match matchMail = Regex.Match(clientemail.Text.Trim(), expression, RegexOptions.IgnoreCase);
        if (!matchMail.Success)
        {
            clientemailLbl.Text = "Incorrect mail format e.g. letenok@dws.gov.za";
            clientemailLbl.Visible = true;
            RequiredFieldValidator2.Visible = false;
        }

        if (!match.Success)
        {
            lblCell.Text = "Cell No. format is incorrect. Only number allowed e.g. 0123365556";
            RequiredFieldValidator3.Visible = false;
            lblCell.Visible = true;
        }
        if (!matchFax.Success)
        {
            lblFax.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
            RequiredFieldValidator4.Visible = false;
            lblFax.Visible = true;
        }
        if (!matchTell.Success)
        {
            lblTel.Text = "Tell No. format is incorrect. Only number allowed e.g. 0123365556";
            RequiredFieldValidator5.Visible = false;
            lblTel.Visible = true;
        }

        else if (matchTell.Success && matchFax.Success && match.Success && matchMail.Success && !SDateEdate)
        {

            if (ValidateDatTime())
            {
                SdateLbl.Text = "";
                CheckBoomaker();
            }

        }


    }

    private bool ValidateDatTime()
    {
        bool StartDateGreater = true;
        IBTCWebService ibtc = new IBTCWebService();
        List<UserDetails> sortedList = ibtc.LoadCourses().OrderBy(x => x._EdateTime).ToList<UserDetails>();
        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);

        if (sortedList.Count > 0)
        {
            string VenueName = DropDownList3.SelectedValue.Split(',')[1];

            foreach (UserDetails item in sortedList)
            {
                DateTime Sdate = Convert.ToDateTime(item._SdateTime.Trim());
                DateTime Edate = Convert.ToDateTime(item._EdateTime.Trim());

                int compare = DateTime.Compare(Sdate1, Edate);
                int compare2 = DateTime.Compare(Edate1, Sdate);

                if (VenueName.Trim() == item._venue.Trim())
                {
                    if (compare2 <= 0 || compare >= 0)
                    {
                        StartDateGreater = true;
                    }
                    else
                    {
                        SdateLbl.Text = "There is a Venue booked inbetween this Date Time.";
                        StartDateGreater = false;
                        string StartDte = SDate.Text;
                        SDate.Text = StartDte;
                        string EndtDte = EDate.Text;
                        EDate.Text = EndtDte;
                        break;
                      
                    }

                }             

            }
        }
       
        return StartDateGreater;
    }

    protected void CheckBoomaker()
    {
        BookieemailLbl.Text = "";
        BookieFaxLbl.Text = "";
        BookiecellLbl.Text = "";
        //BookMaker Number validation
        Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        Match match = regex.Match(BookieFax.Text.Trim());
        Match matchFax = regex.Match(Bookiecell.Text.Trim());
        //Match matchTell = regex.Match(Tell.Text);
        string expression = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

        Match matchMail = Regex.Match(Bookieemail.Text.Trim(), expression, RegexOptions.IgnoreCase);
        //Regex regxMail = new Regex("^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
        //  Match matchMail = regxMail.Match(BookieemailLbl.Text);

        if (!matchMail.Success)
        {
            BookieemailLbl.Text = "Incorrect mail format e.g. letenok@dws.gov.za";
            BookieemailLbl.Visible = true;
            RequiredFieldValidator6.Visible = false;
        }


        if (!match.Success)
        {
            BookieFaxLbl.Text = "Cell No. format is incorrect. e.g. 0123365556";
            RequiredFieldValidator12.Visible = false;
            BookieFaxLbl.Visible = true;
        }
        if (!matchFax.Success)
        {
            BookiecellLbl.Text = "Fax No. format is incorrect. e.g. 0123365556";
            RequiredFieldValidator13.Visible = false;
            BookiecellLbl.Visible = true;
        }

        else if (matchFax.Success && match.Success && matchMail.Success)
        {
            BookClient("venue");
        }
        //if (!matchTell.Success)
        //{
        //    lblTel.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
        //    RequiredFieldValidator5.Visible = false;
        //    lblTel.Visible = true;
        //}

    }

    protected void BookVenueMethod(int ClientID, int BoomakerID)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCVenue(Type,Visibile, SDate,EDate,ClientID,BookMakerID,Time_stamp) values(@Type, @Visibile, @SDate,@EDate,@ClientID,@BookMakerID,@timestamp) select @id=SCOPE_IDENTITY()", con);
             SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;
        //  string DateFormat = "dd-MM-yyyy hh:mm:ss";
        //string Type = Request.Form["DropDownList3"];
        DateTime Sdate = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate = Convert.ToDateTime(Request.Form["EDate"]);
        string VenueType = DropDownList3.SelectedItem.Text;

        insert.Parameters.AddWithValue("@Type", VenueType);
         insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@SDate", Sdate);
        insert.Parameters.AddWithValue("@Visibile", "true");
        insert.Parameters.AddWithValue("@EDate", Edate);
        insert.Parameters.AddWithValue("@ClientID", ClientID);
        insert.Parameters.AddWithValue("@BookMakerID", BoomakerID);
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);

      
        try
        {
            con.Close();
            con.Open();
            int succes = insert.ExecuteNonQuery();
            if (succes == 1)
            {
              //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('Venue has been succesfully booked');", true);
                 lastID = Convert.ToInt32(id.Value);
                 BookCourseMethod(ClientID, BoomakerID, lastID);
              //  Response.Write("<script>alert('Venue has been succesfully booked')</script>");
                SDate.Text = "";
                EDate.Text = "";
                BookieemailLbl.Text = "";
                BookieFaxLbl.Text = "";
                BookiecellLbl.Text = "";
                clientemailLbl.Text = "";
                lblCell.Text = "";
                lblFax.Text = "";
                lblTel.Text = "";
                DropDownList3.SelectedIndex = 0;
                clientName.Text = "";
                clientsurname.Text = "";
                clientemail.Text = "";
                Cell.Text = "";
                Fax.Text = "";
                Tell.Text = "";
                Bookiename.Text = "";
                Bookiesurname.Text = "";
                Bookieemail.Text = "";
                BookieFax.Text = "";
                Bookiecell.Text = "";
                RequiredFieldValidator3.Visible = true;
                RequiredFieldValidator4.Visible = true;
                RequiredFieldValidator5.Visible = true;
                sdtlbl.Visible = true;
                RequiredFieldValidator9.Visible = true;
                RequiredFieldValidator8.Visible = true;
                RequiredFieldValidator6.Visible = true;
                RequiredFieldValidator7.Visible = true;
                RequiredFieldValidator13.Visible = true;
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
           // Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }
    }

    protected void BookClient(string TypeOfBooking)
    {
        SqlCommand insert = new SqlCommand("insert into IBTClient(Name, Surname, Phone_Number,Email,Fax_no,Tel_no,Time_stamp) values(@Name, @Surname, @Phone_Number,@Email,@Fax_no,@Tel_no,@timestamp); select @id=SCOPE_IDENTITY()", con);

        //string ClientName = Request.Form["name"];
        //string ClientSurname = Request.Form["Surname"];
        //string Tell_Number = Request.Form["Tell"];
        //string Email = Request.Form["email"];
        //string Cell = Request.Form["Cell"];
        //string Fax_no = Request.Form["Fax"];
        int succes = 0;
        //string Sdate = Request.Form["Sdate"];
        //string EDate = Request.Form["EDate"];

        SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;


        insert.Parameters.AddWithValue("@Name", clientName.Text);
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", clientsurname.Text);
        insert.Parameters.AddWithValue("@Email", clientemail.Text);
        insert.Parameters.AddWithValue("@Phone_Number", Cell.Text);
        insert.Parameters.AddWithValue("@Tel_no", Tell.Text);
        insert.Parameters.AddWithValue("@Fax_no", Fax.Text);
        //insert.Parameters.AddWithValue("@Sdate", Sdate);
        //insert.Parameters.AddWithValue("@EDate", EDate);
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);


        try
        {
            con.Close();
            con.Open();
            succes = insert.ExecuteNonQuery();
            if (succes == 1)
            {
                lastID = Convert.ToInt32(id.Value);
                InsertBookMaker(lastID, TypeOfBooking);
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
            //Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

    }

    protected void BookVenue(object sender, EventArgs e)
    {
        Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        Match match = regex.Match(Cell.Text);
        Match matchFax = regex.Match(Fax.Text);
        Match matchTell = regex.Match(Tell.Text);
        if (!match.Success)
        {
            lblCell.Text = "Cell No. format is incorrect. Only number allowed e.g. 0123365556";
        }
        if (!matchTell.Success)
        {
            lblTel.Text = "Tell No. format is incorrect. Only number allowed e.g. 0123365556";
        }
        if (!matchFax.Success)
        {
            lblFax.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
        }
        else
        {
            string ftype = "Venue";
            BookClient(ftype);
        }

    }

    protected int InsertBookMaker(int clientid, string TypeOfBooking)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCBookMaker(Name, Surname,Email,Fax_no,Tel_no,Time_stamp) values(@Name,@Surname,@Email,@Fax_no,@Tel_no,@timestamp);select @id=SCOPE_IDENTITY()", con);

        SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;
        int success = 0;

        //string Name = Request.Form["BookMakername"];
        //string Surname = Request.Form["BookMakerSurname"];
        //string email = Request.Form["BookMakerEmail"];
        //string Fax = Request.Form["BookMakerFaxNo"];
        //string Tell = Request.Form["BookMakerTellNo"];

      
        insert.Parameters.AddWithValue("@Name", Bookiename.Text);
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", Bookiesurname.Text);
        insert.Parameters.AddWithValue("@Email", Bookieemail.Text);
        insert.Parameters.AddWithValue("@Fax_no", BookieFax.Text);
        insert.Parameters.AddWithValue("@Tel_no", BookieFax.Text);
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);

        try
        {
            con.Close();
            con.Open();

            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
                lastID = Convert.ToInt32(id.Value);
                if (TypeOfBooking == "venue")
                {
                     
                    BookVenueMethod(clientid, lastID);
                }
                //else
                //{
                //    BookCourseMethod(clientid, lastID);
                //}
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
           // Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return lastID;
    }

    protected void BookCourseMethod(int ClientID, int BoomakerID, int VenueID)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCCourse(Visible, SDate,EDate,NumOfAttendees,ClientID,VenueID,BookMakerID,Time_stamp,EventID) values(@Visible,@SDate,@EDate,@NumOfAttendees,@ClientID,@VenueID,@BookMakerID,@timestamp,@EventID)", con);
        SqlCommand select = new SqlCommand("Select VenueID from IBTCVenue where VenueID = @VenueID", con);

        select.Parameters.AddWithValue("@VenueID", VenueID);
        DataTable dt = new DataTable();
        SqlDataAdapter dql = new SqlDataAdapter(select);
        dql.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string DropDownValue = Request.Form["DropDownList2"];
            DateTime Sdate = Convert.ToDateTime(Request.Form["SDate"]);
            DateTime Edate = Convert.ToDateTime(Request.Form["EDate"]);
            string NumOfAttendees = Request.Form["attendees"];
            string EventID = DropDownValue.Split(',')[0];
            // string Title = DropDownValue.Split(' ')[1];

            insert.Parameters.AddWithValue("@VenueID", VenueID);
            insert.Parameters.AddWithValue("@EventID", EventID);
            insert.Parameters.AddWithValue("@BookMakerID", BoomakerID);
            // insert.Parameters.AddWithValue("@Title", Title);
            insert.Parameters.AddWithValue("@NumOfAttendees", NumOfAttendees);
            insert.Parameters.AddWithValue("@SDate", Sdate);
            insert.Parameters.AddWithValue("@EDate", Edate);
            insert.Parameters.AddWithValue("@ClientID", ClientID);
            insert.Parameters.AddWithValue("@timestamp", DateTime.Now);
            insert.Parameters.AddWithValue("@Visible", "True");


            try
            {

                con.Close();
                con.Open();
                int succes = insert.ExecuteNonQuery();
                if (succes == 1)
                {
                    //  Response.Write("<script>alert('The Event has been succesfully booked')</script>");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('The Event has been succesfully booked');", true);
                    SmtpClient client = new SmtpClient("10.132.32.78");

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("webmaster@dws.gov.za", "Webmaster");
                    message.To.Add(clientemail.Text.Trim());
                    //message.To.Add("molekwam@dws.gov.za");
                    //message.To.Add("kgoputsos@dws.gov.za");
                    //message.To.Add("Govenderv@dws.gov.za");
                    //message.To.Add("Tshobenih@dws.gov.za");


                    message.Subject = "IBTC Venue Booking";

                    message.IsBodyHtml = true;

                    message.Body += "<p>Good day</p>" + "<p>This is to confirm that the Venue '" + DropDownList3.SelectedValue + "' has been booked for the Start Date: '" + SDate.Text +" untill the End Date '"+ EDate.Text+"' </p><p> Kind Regards IBTC Team'</p>";


                    //message.Body += "<table border='1' background-color = '#A9A9A9' max-width = '300px'><tr><th max-width='50'>Course</th><th max-width='50'>Did the programme fulfil your personal development objectives?</th><th max-width='50'>Facility</th><th max-width='50'>Would you use the IBTC facility again or would you refer a colleague?</th><th max-width='50'>Comments</th><th max-width='50'>Suggestions</th></tr> " +
                    //" <tr><td max-width='50'>" + DDLSelectCourse.Text + "</td><td max-width='50'>" + RadioButtonList4.Text + " </td><td max-width='50'>" + DDLFacilities.Text + "</td><td max-width='50'>" + RadioButtonList1.Text + RadioButtonList2.Text + RadioButtonList3.Text + "</td><td max-width='50'>" + txtComments.Text + "</td><td max-width='50'>" + txtSuggestions.Text + "</td></tr></table>";

                    client.EnableSsl = false;
                    client.Send(message);
                    //clientemailLbl.Text = "";
                    //lblCell.Text = "";
                    //lblFax.Text = "";
                    //lblTel.Text = "";
                    SDate.Text = "";
                    EDate.Text = "";
                    BookieemailLbl.Text = "";
                    BookieFaxLbl.Text = "";
                    BookiecellLbl.Text = "";
                    BookieemailLbl.Text = "";
                    BookieFaxLbl.Text = "";
                    BookiecellLbl.Text = "";
                    attendees.Text = "";
                    DropDownList2.SelectedIndex = 0;
                    DropDownList3.SelectedIndex = 0;

                   // attendees.Text = "";
                    //clientemailLbl.Text = "";
                    //lblCell.Text = "";
                    //lblFax.Text = "";
                    //lblTel.Text = "";
                 //   DropDownList2.SelectedIndex = 0;
                //    DropDownList3.SelectedIndex = 0;
                    clientName.Text = "";
                    clientsurname.Text = "";
                    //clientemail.Text = "";
                    //Cell.Text = "";
                    //Fax.Text = "";
                    //Tell.Text = "";
                    Bookiename.Text = "";
                    Bookiesurname.Text = "";
                    Bookieemail.Text = "";
                    BookieFax.Text = "";
                    Bookiecell.Text = "";
                    //   userLbl.Text = "";
              //      Label1.Text = "";
                    //RequiredFieldValidator5.Visible = true;
                    //lblTel.Visible = false;
                    //clientemailLbl.Visible = false;
                    //RequiredFieldValidator2.Visible = true;
                    //RequiredFieldValidator3.Visible = true;
                    //lblCell.Visible = false;
                    //RequiredFieldValidator4.Visible = true;
                    //lblFax.Visible = false;
                    //RequiredFieldValidator5.Visible = true;
                    //lblTel.Visible = false;
                    sdtlbl.Visible = true;
                    RequiredFieldValidator6.Visible = true;
                    RequiredFieldValidator12.Visible = true;
                    RequiredFieldValidator13.Visible = true;
                //    attendees.Text = "";
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
                //Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
            }

            finally
            {
                con.Close();
            }
        }

     
    }
}