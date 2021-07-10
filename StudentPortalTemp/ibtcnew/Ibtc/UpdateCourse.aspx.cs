using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateCourse : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    IBTCWebService retrieveVenues = new IBTCWebService();
    List<UserDetails> user = new List<UserDetails>();
    // private static string initialVenueID;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = retrieveVenues.LoadCourses();
        SdateLbl.Text = "";
        if (!IsPostBack)
        {
            Session["username"] = clientName.Text;
            getCourse();

        }
    }

    private int id
    {

        get
        {
            return !string.IsNullOrEmpty(Request.QueryString["CourseId"]) ? int.Parse(Request.QueryString["CourseId"]) : 0;
        }
    }

    private bool checkVenueDatTime(string venueID)
    {

        DateTime Sdate1 = Convert.ToDateTime(SDate.Text.Trim());
        DateTime Edate1 = Convert.ToDateTime(Edate.Text.Trim());
        bool equivalent = true;

        SqlCommand select = new SqlCommand("Select SDate, EDate from IBTCVenue where venueID = @VenueID", con);

        select.Parameters.Add("@VenueID", venueID);

        DataTable dt = new DataTable();
        SqlDataAdapter sqa = new SqlDataAdapter(select);
        sqa.Fill(dt);
        DateTime VenueSDAteTime = Convert.ToDateTime(dt.Rows[0]["SDate"].ToString());
        DateTime VenueEDAteTime = Convert.ToDateTime(dt.Rows[0]["EDate"].ToString());
        if (Sdate1 == VenueSDAteTime && Edate1 == VenueEDAteTime)
        {
            equivalent = true;
        }
        else
        {
            string StartDte = SDate.Text;
            SDate.Text = StartDte;
            string EndtDte = Edate.Text;
            Edate.Text = EndtDte;
            SdateLbl.Text = "The Selected Datetime does not match the scheduled Venues";
            equivalent = false;
        }
        //foreach (UserDetails item in user)
        //{
        //    if (clientName.Text == item._name && clientsurname.Text == item._surname)
        //    {
        //        if (DropDownList1.SelectedItem.Text == item._venue)
        //        {
        //            if (Sdate1.ToString() == item._SdateTime && Edate1.ToString() == item._EdateTime)
        //            {
        //                equivalent = true;
        //                break;
        //            }
        //            else
        //            {
        //                string StartDte = SDate.Text;
        //                SDate.Text = StartDte;
        //                string EndtDte = Edate.Text;
        //                Edate.Text = EndtDte;
        //                SdateLbl.Text = "The Selected Datetime does not match the scheduled Venues";
        //                equivalent = false;
        //                break;
        //            }
        //        }
        //    }

        return equivalent;
    }


    private void getCourse()
    {
        IBTCWebService tbtc = new IBTCWebService();
        List<courseAttr> ud = tbtc.LoadCourseEdit(id);
        string courseName = Request.QueryString["_noAttendees"];
        string VenueInitial = "";

        foreach (courseAttr item in ud)
        {
            clientName.Text = item._name.Trim();
            clientsurname.Text = item._surname.Trim();
            clientemail.Text = item._email.Trim();
            Tell.Text = item._Tell.Trim();
            Cell.Text = item._Cell.Trim();
            Fax.Text = item._Fax.Trim();
            attendees.Text = item._noAttendees.Trim();
            //ListItem li = new ListItem();
            //li.Text = item._venue;
            //li.Value = item._VenueID;
            //DropDownList1.Items.Add(li);
            VenueInitial = item._VenueID;
            SDate.Text = item._SdateTime.Trim();
            Edate.Text = item._EdateTime.Trim();
            Bookiename.Text = item.BookMakerName.Trim();
            Bookiesurname.Text = item.BookMakerSurName.Trim();
            Bookieemail.Text = item.BookMakerEmail.Trim();
            Bookiecell.Text = item.BookMakerTel_no.Trim();
            BookieFax.Text = item.BookMakerFax_no.Trim();

            //            clientsurname.Text = item.
            //clientName.te 
        }
        List<EventsAttr> Ea = tbtc.LoadEvents();

        List<UserDetails> Venues = tbtc.LoadCourses();
        string initialValue = LoadIniValue();

        DropDownList2.DataTextField = "EventName";
        DropDownList2.DataValueField = "EventIDName";
        DropDownList2.DataSource = Ea;
        DropDownList2.DataBind();


        DropDownList1.DataTextField = "_venue";
        DropDownList1.DataValueField = "_VenueID";
        DropDownList1.DataSource = Venues;
        DropDownList1.DataBind();

        foreach (ListItem item in DropDownList1.Items)
        {
            if (item.Value == VenueInitial)
            {
                item.Selected = true;
                break;
            }
        }
        ViewState["VenueID"] = DropDownList1.SelectedItem.Value;
        foreach (ListItem item in DropDownList2.Items)
        {
            if (item.Text.Trim() == initialValue)
            {
                item.Selected = true;
                break;
            }
        }
        //  DropDownList2.SelectedValue = DropDownList2.Items.FindByText(initialValue).ToString();

        //  DropDownList2.SelectedValue = initialValue;



    }

    private string LoadIniValue()
    {
        string eventName = "";
        SqlCommand selectCourse = new SqlCommand("SELECT Event_Name From IBTCEvents inner join IBTCCourse on IBTCEvents.EventID = IBTCCourse.EventID where CoursID = @CourseID", con);

        selectCourse.Parameters.Add("@CourseID", id);
        DataTable dt = new DataTable();

        SqlDataAdapter sqa = new SqlDataAdapter(selectCourse);
        sqa.Fill(dt);
        eventName = dt.Rows[0]["Event_name"].ToString();
        return eventName;

    }

    protected void btn5_Click(object sender, EventArgs e)
    {
        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        int compareDate = DateTime.Compare(Edate1, Sdate1);
        bool SDateEdate = false;
        //  bool VenueHasCourse = CheckBookedVenues(DropDownList1.SelectedValue.ToString(), ViewState["VenueID"].ToString());

        //if (VenueHasCourse)
        //{
        //    userLbl.Text = "Venue has been booked for an Event";
        //    userLbl.Visible = true;
        //}
        //else
        //{
        if (!ValidateDatTime())
        {

        }
        else
        {
            userLbl.Visible = false;
            int bookmakerID;
            int ClientID;

            if (compareDate < 0)
            {
                EDateLbl.Text = "Booking End Date must be greater than Booking Start Date";
                SDateEdate = true;
                string StartDte = SDate.Text;
                SDate.Text = StartDte;
                string EndtDte = Edate.Text;
                Edate.Text = EndtDte;
            }
            else
            {
                EDateLbl.Text = "";
                SDateEdate = false;
            }

            if (!SDateEdate)
            {
                bool Validatedd = validation();
                if (Validatedd)
                {
                    if (checkVenueDatTime(DropDownList1.SelectedValue.ToString()))
                    {
                        bool CheckBookerExists = BookerExists();
                        bool CheckClientExists = ClientExists();

                        if (!CheckBookerExists)
                        {
                            bookmakerID = InsertBookMaker();
                        }
                        else
                        {
                            bookmakerID = UpdateBookMaker();
                        }

                        if (!CheckClientExists)
                        {
                            InsertClient(bookmakerID);
                        }
                        else
                        {
                            UpdateClient(bookmakerID);
                        }
                    }
                }
            }
            // }

        }
        //IBTCWebService retrieveVenues = new IBTCWebService();
        // List<UserDetails> user = retrieveVenues.UpdateCourse();

    }

    private bool CheckBookedVenues(string Venueid, string initialVenueID)
    {
        bool VenueExists = false;
        bool StartDateGreater = false;
        SqlCommand select = new SqlCommand("Select NumOfAttendees from IBTCCourse where VenueID = @VenueID and Visible = @Visible", con);

        select.Parameters.Add("@VenueID", Venueid);
        select.Parameters.Add("@Visible", "true");

        con.Close();
        con.Open();

        DataTable dt = new DataTable();

        SqlDataAdapter sqa = new SqlDataAdapter(select);
        sqa.Fill(dt);

        if (dt.Rows.Count > 0 && initialVenueID != Venueid)
        {
            VenueExists = true;
            //foreach (courseAttr item in dt.Rows)
            //{
            //    DateTime Sddate = Convert.ToDateTime(SDate.Text);
            //    DateTime EDdate = Convert.ToDateTime(Edate.Text);

            //    DateTime Sdate = Convert.ToDateTime(item._SdateTime);
            //    DateTime EDate = Convert.ToDateTime(item._EdateTime);

            //    int compare = DateTime.Compare(Sddate, EDate);
            //    int compare2 = DateTime.Compare(EDdate, Sdate);

            //    if (compare2 <= 0 || compare >= 0)
            //    {
            //        StartDateGreater = true;
            //    }
            //    else
            //    {
            //        string StartDte = SDate.Text;
            //        SDate.Text = StartDte;
            //        string EndtDte = Edate.Text;
            //        Edate.Text = EndtDte;
            //        SdateLbl.Text = "There is an Event scheduled inbetween this Date Time.";
            //        StartDateGreater = false;
            //        break;

            //    }
            //}

        }
        else
        {
            VenueExists = false;
        }
        return VenueExists;
    }

    private void UpdateClient(int BookmakerID)
    {
        SqlCommand UpdateClient = new SqlCommand("Update IBTClient SET Email= @email, phone_number = @phone_number, Fax_no = @fax_no, Tel_no = @Tell_no, @clientID = clientID, Time_stamp = @Time_stamp where name = @name", con);
        SqlParameter id = new SqlParameter("ClientID", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;

        UpdateClient.Parameters.Add("@email", clientemail.Text.Trim());
        UpdateClient.Parameters.Add("@phone_number", Cell.Text.Trim());
        UpdateClient.Parameters.Add("@fax_no", Fax.Text.Trim());
        UpdateClient.Parameters.Add("@Tell_no", Tell.Text.Trim());
        UpdateClient.Parameters.Add("@Time_stamp", DateTime.Now);
        UpdateClient.Parameters.Add("@name", clientName.Text.Trim());
        UpdateClient.Parameters.Add(id);

        //  string ClientID = "";

        try
        {
            con.Close();
            con.Open();
            int Update = UpdateClient.ExecuteNonQuery();
            if (Update >= 1)
            {
                lastID = Convert.ToInt32(id.Value);
                UpdateCourseMethod(lastID, BookmakerID);
                //     Response.Write("<script>alert('')</script>");

            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
            //  Response.Write("<script>alert('Error 501:Please Contact Web Administrator')</script>");
        }
    }

    private bool ClientExists()
    {
        bool ClientExists = false;
        SqlCommand selectVenue = new SqlCommand("SELECT name,surname From IBTClient where name = @name and surname = @surname", con);

        selectVenue.Parameters.Add("@name", clientName.Text.Trim());
        selectVenue.Parameters.Add("@surname", clientsurname.Text.Trim());

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(selectVenue);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ClientExists = true;
        }
        return ClientExists;
    }

    private bool InsertClient(int bookmakerID)
    {
        bool inserted = false;
        SqlCommand insert = new SqlCommand("insert into IBTClient(Name, Surname,Email,Fax_no,Tel_no, Phone_Number,Time_stamp) values(@Name,@Surname,@Email,@Phone_Number,@Fax_no,@Tel_no,@timestamp);select @id=SCOPE_IDENTITY()", con);

        SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;
        int success = 0;

        //string Name = Request.Form["BookMakername"];
        //string Surname = Request.Form["BookMakerSurname"];
        //string email = Request.Form["BookMakerEmail"];int clientid, string TypeOfBooking, int VenueID
        //string Fax = Request.Form["BookMakerFaxNo"];
        //string Tell = Request.Form["BookMakerTellNo"];


        insert.Parameters.AddWithValue("@Name", clientName.Text.Trim());
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", clientsurname.Text.Trim());
        insert.Parameters.AddWithValue("@Email", clientemail.Text.Trim());
        insert.Parameters.AddWithValue("@Fax_no", Fax.Text.Trim());
        insert.Parameters.AddWithValue("@Tel_no", Tell.Text.Trim());
        insert.Parameters.AddWithValue("@Phone_Number", Cell.Text.Trim());
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);

        try
        {
            con.Close();
            con.Open();

            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
                inserted = false;
                lastID = Convert.ToInt32(id.Value);

                UpdateCourseMethod(lastID, bookmakerID);

                //else
                //{
                //    BookCourseMethod(clientid, lastID);
                //}
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
            //    Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return inserted;
        //       inserted = true;
    }

    private void UpdateCourseMethod(int cliendID, int BookMakerID)
    {
        SqlCommand UpdateClient = new SqlCommand("Update IBTCCourse SET NumOfAttendees = @NumOfAttendees, SDate = @SDate,EDate = @EDate, VenueID = @VenueID, ClientID = @ClientID, EventID = @EventID,BookmakerID = @BookmakerID, Time_stamp = @Time_stamp where CoursID = @CoursID", con);

        string eventID = DropDownList2.SelectedValue.Split(',')[0];

        UpdateClient.Parameters.Add("SDate", SDate.Text.Trim());
        UpdateClient.Parameters.Add("EDate", Edate.Text.Trim());
        UpdateClient.Parameters.Add("@CoursID", id);
        UpdateClient.Parameters.Add("@VenueID", DropDownList1.SelectedValue);
        ////UpdateClient.Parameters.Add("@EDate", Edate.Text);
        UpdateClient.Parameters.Add("@NumOfAttendees", attendees.Text.Trim());
        UpdateClient.Parameters.Add("@ClientID", cliendID);
        UpdateClient.Parameters.Add("@EventID", eventID);
        UpdateClient.Parameters.Add("@BookmakerID", BookMakerID);

        UpdateClient.Parameters.Add("@Time_stamp", DateTime.Now);

        try
        {
            int success = UpdateClient.ExecuteNonQuery();
            if (success == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('The Event has been successfully updated.');", true);
                //  Response.Write("<script>alert('The Event has been successfully updated.')</script>");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private bool ValidateDatTime()
    {
        bool StartDateGreater = true;
        IBTCWebService ibtc = new IBTCWebService();
        List<courseAttr> sortedListCourses = ibtc.LoadVenues().OrderBy(x => x._EdateTime).ToList<courseAttr>();

        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        // int clientID;
        string[] CourseNames = DropDownList2.SelectedValue.Split(' ');
        //foreach (string item in CourseNames)
        //{
        //    bool result = int.TryParse(item, out clientID);
        //    if (result == true)
        //    {
        //        break;
        //    }
        //}
        string CourseName = DropDownList2.SelectedValue.Split(',')[1];
        StartDateGreater = ValidateDateTimeOverlap(StartDateGreater, sortedListCourses, Sdate1, Edate1, CourseName);
        return StartDateGreater;

    }

    private bool ValidateDateTimeOverlap(bool StartDateGreater, List<courseAttr> sortedList, DateTime Sdate1, DateTime Edate1, string CourseName)
    {
        foreach (courseAttr item in sortedList)
        {
            DateTime Sdate = Convert.ToDateTime(item._SdateTime);
            DateTime EDate = Convert.ToDateTime(item._EdateTime);

            int compare = DateTime.Compare(Sdate1, EDate);
            int compare2 = DateTime.Compare(Edate1, Sdate);

            SdateLbl.Text = "";
            if (DropDownList1.SelectedItem.Text.Trim() == item._venue.Trim() && DropDownList1.SelectedValue != ViewState["VenueID"].ToString())
            {

                //if (CourseName.Trim() == item.courseName)
                //{

                if (compare2 <= 0 || compare >= 0)
                {
                    StartDateGreater = true;
                }
                else
                {
                    string StartDte = SDate.Text;
                    SDate.Text = StartDte;
                    string EndtDte = Edate.Text;
                    Edate.Text = EndtDte;
                    SdateLbl.Text = "There is an Event scheduled inbetween this Date Time.";
                    StartDateGreater = false;
                    break;

                }
            }

            // }
            //}
        }

        return StartDateGreater;
    }


    private int UpdateBookMaker()
    {
        SqlCommand UpdateBookMaker = new SqlCommand("Update IBTCBookMaker SET Email= @email, surname = @surname, Fax_no = @fax_no, Tel_no = @Tell_no, @BookMakerID = BookmakerID, Time_stamp = @Time_stamp where name = @name;", con);
        SqlParameter id = new SqlParameter("BookMakerID", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;
        //   string bookmmakerID = "";
        UpdateBookMaker.Parameters.Add("@email", Bookieemail.Text.Trim());
        UpdateBookMaker.Parameters.Add("@fax_no", BookieFax.Text.Trim());
        UpdateBookMaker.Parameters.Add("@Tell_no", Bookiecell.Text.Trim());
        UpdateBookMaker.Parameters.Add("@Time_stamp", DateTime.Now);
        UpdateBookMaker.Parameters.Add("@name", Bookiename.Text.Trim());
        UpdateBookMaker.Parameters.Add("@surname", Bookiesurname.Text.Trim());
        UpdateBookMaker.Parameters.Add(id);
        //  UpdateBookMaker.Parameters.Add("@BookMakerID", bookmmakerID);
        try
        {
            con.Close();
            con.Open();
            int Update = UpdateBookMaker.ExecuteNonQuery();
            if (Update >= 1)
            {
                lastID = Convert.ToInt32(id.Value);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('Error 501:Please Contact Web Administrator');", true);
            //   Response.Write("<script>alert('Error 501:Please Contact Web Administrator')</script>");
        }
        return lastID;
    }

    private bool validation()
    {
        bool Validated = CheckBoomaker();
        if (Validated)
        {
            Validated = CheckClient();
        }

        return Validated;
    }
    private bool CheckBoomaker()
    {
        bool validated = false;
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
            BookieFaxLbl.Text = "Cell No. format is incorrect. Only number allowed e.g. 0123365556";
            RequiredFieldValidator12.Visible = false;
            BookieFaxLbl.Visible = true;
        }
        if (!matchFax.Success)
        {
            BookiecellLbl.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
            RequiredFieldValidator13.Visible = false;
            BookiecellLbl.Visible = true;
        }

        if (matchFax.Success && match.Success && matchMail.Success)
        {
            validated = true;
        }
        //if (!matchTell.Success)
        //{
        //    lblTel.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
        //    RequiredFieldValidator5.Visible = false;
        //    lblTel.Visible = true;
        //}
        return validated;
    }

    private bool CheckClient()
    {
        bool validated = false;
        clientemailLbl.Text = "";
        lblCell.Text = "";
        lblTel.Text = "";
        lblFax.Text = "";
        Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        Match match = regex.Match(Fax.Text.Trim());
        Match matchFax = regex.Match(Tell.Text.Trim());
        Match matchCell = regex.Match(Cell.Text.Trim());

        string expression = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

        Match matchMail = Regex.Match(clientemail.Text.Trim(), expression, RegexOptions.IgnoreCase);

        //    Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        //Match match = regex.Match(Cell.Text);
        //  Match matchFax = regex.Match(Fax.Text);
        Match matchTell = regex.Match(Tell.Text.Trim());
        if (!matchMail.Success)
        {
            clientemailLbl.Text = "Incorrect mail format e.g. letenok@dws.gov.za";
            clientemailLbl.Visible = true;
            RequiredFieldValidator2.Visible = false;
        }
        if (!matchCell.Success)
        {
            lblCell.Text = "Cell No. format is incorrect. Only number allowed e.g. 0123365556";
            lblCell.Visible = true;
            RequiredFieldValidator5.Visible = false;
        }
        if (!matchTell.Success)
        {
            lblTel.Text = "Tell No. format is incorrect. Only number allowed e.g. 0123365556";
            lblTel.Visible = true;
            RequiredFieldValidator5.Visible = false;

        }
        if (!match.Success)
        {
            lblFax.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
            lblFax.Visible = true;
            RequiredFieldValidator4.Visible = false;
        }
        if (matchFax.Success && matchTell.Success && matchMail.Success)
        {
            validated = true;
        }
        return validated;
    }

    protected int InsertBookMaker()
    {

        SqlCommand insert = new SqlCommand("insert into IBTCBookMaker(Name, Surname,Email,Fax_no,Tel_no,Time_stamp) values(@Name,@Surname,@Email,@Fax_no,@Tel_no,@timestamp);select @id=SCOPE_IDENTITY()", con);

        SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;
        int success = 0;



        insert.Parameters.AddWithValue("@Name", Bookiename.Text.Trim());
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", Bookiesurname.Text.Trim());
        insert.Parameters.AddWithValue("@Email", Bookieemail.Text.Trim());
        insert.Parameters.AddWithValue("@Fax_no", BookieFax.Text.Trim());
        insert.Parameters.AddWithValue("@Tel_no", Bookiecell.Text.Trim());
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);

        try
        {
            con.Close();
            con.Open();

            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
                lastID = Convert.ToInt32(id.Value);
                //BookCourseMethod(clientid, lastID, VenueID);

                //else
                //{
                //    BookCourseMethod(clientid, lastID);
                //}
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('Error 501:Please Contact Web Administrator');", true);
            //   Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return lastID;
    }

    private bool BookerExists()
    {
        bool BookMakerExists = false;
        SqlCommand selectVenue = new SqlCommand("SELECT name,surname From IBTCBookMaker where name = @name and surname = @surname", con);

        selectVenue.Parameters.Add("@name", Bookiename.Text.Trim());
        selectVenue.Parameters.Add("@surname", Bookiesurname.Text.Trim());

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(selectVenue);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            BookMakerExists = true;
        }
        return BookMakerExists;
    }
}