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
    protected void Page_Load(object sender, EventArgs e)
    {

       // user = retrieveVenues.LoadCourses();
        SdateLbl.Text = "";
        if (!IsPostBack)
        {
         
            List<EventsAttr> ae = retrieveVenues.LoadEvents();
            DropDownList1.DataTextField = "EventName";
            DropDownList1.DataValueField = "EventIDName";
            DropDownList1.DataSource = ae;
            DropDownList1.DataBind();
            string id = Request.QueryString["VenueID"];
            getVenues();

            //Session["username"] = clientName.Text;
        }
    }

    private int id
    {

        get
        {
            return !string.IsNullOrEmpty(Request.QueryString["VenueID"]) ? int.Parse(Request.QueryString["VenueID"]) : 0;
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
            string VenueName = DropDownList2.SelectedValue.Split(',')[1];


            foreach (UserDetails item in sortedList)
            {
                DateTime Sdatte = Convert.ToDateTime(item._SdateTime.Trim());
                DateTime Edatte = Convert.ToDateTime(item._EdateTime.Trim());

                if (id.ToString() != item._VenueID)
                {
                    if (VenueName.Trim() == item._venue.Trim())
                    {
                        int compare = DateTime.Compare(Sdate1, Edatte);
                        int compare2 = DateTime.Compare(Edate1, Sdatte);

                        if (compare2 <= 0 || compare >= 0)
                        {
                            StartDateGreater = true;
                        }
                        else
                        {
                            SdateLbl.Text = "There is a Venue booked in between this Date Time.";
                            StartDateGreater = false;
                            string StartDte = SDate.Text;
                            SDate.Text = StartDte;
                            string EndtDte = Edate.Text;
                            Edate.Text = EndtDte;
                            break;

                        }
                    }
                }

            }
        }

        return StartDateGreater;
    }

    private void getVenues()
    {
        IBTCWebService tbtc = new IBTCWebService();
        List<UserDetails> ud = tbtc.LoadVenuesEdit(id);
        // string courseName = Request.QueryString["_noAttendees"];
        
        foreach (UserDetails item in ud)
        {
            clientName.Text = item._name.Trim();
            clientsurname.Text = item._surname.Trim();
            clientemail.Text = item._email.Trim();
            Tell.Text = item._Cell.Trim();
            Fax.Text = item._Tell.Trim();
            Cell.Text = item._Cell.Trim();
           attendees.Text = item._noAttendees;
            SDate.Text = item._SdateTime.Trim();
            Edate.Text = item._EdateTime.Trim();
            Session["courseID"] = item.CourseID;
            //Bookiename.Text = item.BookMakerName.Trim();
            //Bookiesurname.Text = item.BookMakerSurName.Trim();
            //Bookieemail.Text = item.BookMakerEmail.Trim();
            //Bookiecell.Text = item.BookMakerTel_no.Trim();
            //BookieFax.Text = item.BookMakerFax_no.Trim();

            //            clientsurname.Text = item.
            //clientName.te 
        }
        List<VenueTypes> Ea = tbtc.LoadVenueTypes();

        string initialValue = LoadIniValue();

        DropDownList2.DataTextField = "VenueName";
        DropDownList2.DataValueField = "VenueTypeIDName";
        DropDownList2.DataSource = Ea;
        DropDownList2.DataBind();
        foreach (ListItem item in DropDownList1.Items)
        {
            if (item.Text == ud[0]._eventName)
            {
                item.Selected = true;
                break;
            }
        }

        foreach (ListItem item in DropDownList2.Items)
        {
            if (item.Text.Trim() == initialValue.Trim())
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
        // SqlCommand selectCourse = new SqlCommand("SELECT VenueName From IBTCEvents inner join IBTCCourse on IBTCEvents.EventID = IBTCCourse.EventID where CoursID = @CourseID", con); 
        SqlCommand selectCourse = new SqlCommand("SELECT Type From IBTCVenue where VenueID = @VenueID", con);

        selectCourse.Parameters.Add("@VenueID", id);
        DataTable dt = new DataTable();

        SqlDataAdapter sqa = new SqlDataAdapter(selectCourse);
        sqa.Fill(dt);
        eventName = dt.Rows[0]["Type"].ToString();
        return eventName;

    }


    protected void btn5_Click(object sender, EventArgs e)
    {

      //  CheckBookedVenues();
        int bookmakerID;
        int ClientID;
        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        bool SDateEdate = false;
        int compareDate = DateTime.Compare(Edate1, Sdate1);
        if (Sdate1 < DateTime.Now)
        {
            SdateLbl.Text = "Booking Start Date cannot be a Past date";
            SDateEdate = true;
            string StartDte = SDate.Text;
            SDate.Text = StartDte;
            string EndtDte = Edate.Text;
            Edate.Text = EndtDte;
        }
        else if (compareDate < 0)
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
        bool Validatedd = validation();
        if (Validatedd && !SDateEdate)
        {
            if (ValidateDatTime())
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

        //IBTCWebService retrieveVenues = new IBTCWebService();
        // List<UserDetails> user = retrieveVenues.UpdateCourse();

    }

  

    private void UpdateClient(int BookmakerID)
    {
        SqlCommand UpdateClient = new SqlCommand("Update IBTClient SET Email= @email, Phone_number = @Phone_Number, Fax_no = @fax_no, Tel_no = @Tel_no, @clientID = clientID, Time_stamp = @Time_stamp where name = @name", con);
        SqlParameter id = new SqlParameter("ClientID", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;

        UpdateClient.Parameters.Add("@email", clientemail.Text.Trim());
        UpdateClient.Parameters.Add("@fax_no", Fax.Text.Trim());
        UpdateClient.Parameters.Add("@Phone_number", Cell.Text.Trim());
        UpdateClient.Parameters.Add("@Tel_no", Tell.Text.Trim());
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
                UpdateVenueMethod(lastID, BookmakerID);
                //     Response.Write("<script>alert('')</script>");

            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
        //    Response.Write("<script>alert('Error 501:Please Contact Web Administrator')</script>");
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
        SqlCommand insert = new SqlCommand("insert into IBTClient(Name, Phone_Number, Surname,Email,Fax_no,Tel_no,Time_stamp) values(@Name,@Phone_Number,@Surname,@Email,@Fax_no,@Tel_no,@timestamp);select @id=SCOPE_IDENTITY()", con);

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
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);
        insert.Parameters.AddWithValue("@Phone_Number", Cell.Text.Trim());
        

        try
        {
            con.Close();
            con.Open();

            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
                inserted = false;
                lastID = Convert.ToInt32(id.Value);

                UpdateVenueMethod(lastID, bookmakerID);

                //else
                //{
                //    BookCourseMethod(clientid, lastID);
                //}
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
          //  Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return inserted;
        //       inserted = true;
    }

    private void UpdateVenueMethod(int cliendID, int BookMakerID)
    {
        SqlCommand UpdateVenue = new SqlCommand("Update IBTCVenue SET SDate= @SDate, EDate = @EDate, Type = @Type, ClientID = @ClientID, BookmakerID = @BookmakerID, Time_stamp = @Time_stamp where VenueID = @VenueID", con);

        string eventID = DropDownList2.SelectedValue.Split(',')[0];

        UpdateVenue.Parameters.Add("@VenueID", id);
        UpdateVenue.Parameters.Add("@Type", DropDownList2.SelectedValue.Split(',')[1]);
        UpdateVenue.Parameters.Add("@SDate", SDate.Text.Trim());
        UpdateVenue.Parameters.Add("@EDate", Edate.Text.Trim());
        //  UpdateClient.Parameters.Add("@NumOfAttendees", attendees.Text);
        UpdateVenue.Parameters.Add("@ClientID", cliendID);
       // UpdateVenue.Parameters.Add("@EventID", eventID);
        UpdateVenue.Parameters.Add("@BookmakerID", BookMakerID);

        UpdateVenue.Parameters.Add("@Time_stamp", DateTime.Now);

        try
        {
            int success = UpdateVenue.ExecuteNonQuery();
            if (success == 1)
            {
                UpdateCourseMethod(cliendID, BookMakerID);
             //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('The Venue has been successfully updated.');", true);
                //Response.Write("<script>alert('The Event has been successfully updated.')</script>");
            }
        }
        catch (Exception)
        {

            throw;
        }
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
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
            BookieFaxLbl.Text = "Cell No. format is incorrect.  e.g. 0123365556";
            RequiredFieldValidator12.Visible = false;
            BookieFaxLbl.Visible = true;
        }
        if (!matchFax.Success)
        {
            BookiecellLbl.Text = "Fax No. format is incorrect. e.g. 0123365556";
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
        lblFax.Text = "";
        clientemailLbl.Text = "";
        lblTel.Text = "";
         lblCell.Text = "";
        Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        Match match = regex.Match(Fax.Text.Trim());
        Match matchFax = regex.Match(Tell.Text.Trim());

        string expression = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

        Match matchMail = Regex.Match(clientemail.Text.Trim(), expression, RegexOptions.IgnoreCase);

        //    Regex regex = new Regex(@"^[0]\d{2}\d{3}\d{4}$");
        //Match match = regex.Match(Cell.Text);
        //  Match matchFax = regex.Match(Fax.Text);
        Match matchTell = regex.Match(Tell.Text.Trim());
        Match matchCell = regex.Match(Cell.Text.Trim());
        
        if (!matchMail.Success)
        {
            clientemailLbl.Text = "Incorrect mail format e.g. letenok@dws.gov.za";
            clientemailLbl.Visible = true;
            RequiredFieldValidator2.Visible = false;
            validated = false;
        }
        //if (!match.Success)
        //{
        //    lblCell.Text = "Cell No. format is incorrect. Only number allowed e.g. 0123365556";
        //}
        if (!matchTell.Success)
        {
            lblTel.Text = "Tell No. format is incorrect. Only number allowed e.g. 0123365556";
            lblTel.Visible = true;
            RequiredFieldValidator5.Visible = false;
            validated = false;
        }

        if (!matchCell.Success)
        {
            lblCell.Text = "Cell No. format is incorrect. Only number allowed e.g. 0123365556";
            lblCell.Visible = true;
            RequiredFieldValidator3.Visible = false;
            validated = false;
        }
        if (!match.Success)
        {
            lblFax.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
            lblFax.Visible = true;
            RequiredFieldValidator4.Visible = false;
            validated = false;
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
          //  Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
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

    private void UpdateCourseMethod(int cliendID, int BookMakerID)
    {
        SqlCommand UpdateClient = new SqlCommand("Update IBTCCourse SET NumOfAttendees = @NumOfAttendees, SDate = @SDate,EDate = @EDate, VenueID = @VenueID, ClientID = @ClientID, EventID = @EventID,BookmakerID = @BookmakerID, Time_stamp = @Time_stamp where CoursID = @CoursID", con);
        string courseID = Session["courseID"].ToString();
        string eventID = DropDownList1.SelectedValue.Split(',')[0];

        UpdateClient.Parameters.Add("SDate", SDate.Text.Trim());
        UpdateClient.Parameters.Add("EDate", Edate.Text.Trim());
        UpdateClient.Parameters.Add("@CoursID", courseID);
        UpdateClient.Parameters.Add("@VenueID", id);
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
                Session["name"] = clientName.Text;
                Session["surname"] = clientsurname.Text;
                Session["email"] = clientemail.Text;
                Session["cell"] = Cell.Text;
                Session["fax"] = Fax.Text;
                Session["Tell"] = Tell.Text;
                Session["email"] = clientemail.Text;
                Session["cell"] = Cell.Text;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('The Event has been successfully updated.');", true);
                //  Response.Write("<script>alert('The Event has been successfully updated.')</script>");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}