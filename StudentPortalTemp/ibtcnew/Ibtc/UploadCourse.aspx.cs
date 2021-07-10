using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadCourse : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    IBTCWebService retrieveVenues = new IBTCWebService();
    List<UserDetails> user = new List<UserDetails>();
    int ClientID;
    int VenueID;

    protected void Page_Load(object sender, EventArgs e)
    {
        SdateLbl.Text = "";
        userLbl.Text = "";
        Label1.Text = "";
        EDateLbl.Text = "";

        user = retrieveVenues.LoadCourses();
        if (!IsPostBack)
        {
           
            DropDownList1.DataSource = user;
            DropDownList1.DataTextField = "_venue";
            DropDownList1.DataValueField = "_NameSurnameID";
         
            List<EventsAttr> Ea = retrieveVenues.LoadEvents();
            DropDownList2.DataTextField = "EventName";
            DropDownList2.DataValueField = "EventIDName";
            DropDownList2.DataSource = Ea;
            DropDownList2.DataBind();

            DropDownList1.DataBind();
        }

    }

    private bool checkVenueDatTime()
    {

        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        bool equivalent = true;

        foreach (UserDetails item in user)
        {
            if (clientName.Text.Trim() == item._name.Trim() && clientsurname.Text.Trim() == item._surname.Trim())
            {
                if (DropDownList1.SelectedItem.Text == item._venue.Trim())
                {
                    if (Sdate1.ToString() == item._SdateTime && Edate1.ToString() == item._EdateTime)
                    {
                        equivalent = true;
                        break;
                    }
                    else
                    {
                        string StartDte = SDate.Text;
                        SDate.Text = StartDte;
                        string EndtDte = EDate.Text;
                        EDate.Text = EndtDte;
                        SdateLbl.Text = "The Selected Datetime does not match the scheduled Venues";
                        SdateLbl.Visible = true;
                        sdtlbl.Visible = false;
                        equivalent = false;
                        break;
                    }
                }
            }
        }
        return equivalent;
    }

    protected void btn5_Click(object sender, EventArgs e)
    {

        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        int compareDate = DateTime.Compare(Edate1, Sdate1);
        bool SDateEdate = false;
        if (Sdate1 < DateTime.Now)
        {
            SdateLbl.Text = "Booking Start Date cannot be a Past date";
            SDateEdate = true;
            string StartDte = SDate.Text;
            SDate.Text = StartDte;
            string EndtDte = EDate.Text;
            EDate.Text = EndtDte;
        }
        else  if (compareDate < 0)
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
     
    
            
        if (compareDate < 0)
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
   
        if (!SDateEdate && CheckBoomaker())
        {
            
            if (CheckBookedVenues() >= 1)
            {
                if (checkVenueDatTime())
                {
                  //  Response.Write("<script>Validation complete.</script>");
                     InsertBookMaker(ClientID, "course", VenueID);
                    //  BookCourseMethod(ClientID,);
                    // CheckBoomaker();
                }
            }
            

        }
        //}

    }
    protected bool CheckBoomaker()
    {
        bool bookmakerClear = false;
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

        else if (match.Success && matchFax.Success && match.Success && matchMail.Success)
        {
            bookmakerClear = true;
         //   BookClient("venue");
        }
        //if (!matchTell.Success)
        //{
        //    lblTel.Text = "Fax No. format is incorrect. Only number allowed e.g. 0123365556";
        //    RequiredFieldValidator5.Visible = false;
        //    lblTel.Visible = true;
        //}
        return bookmakerClear;
    }

    protected int CheckBookedVenues()
    {
        int success = 0;
        // string Title = Request.Form["DropDownList1"];
        string TextFieldNameSurname = clientsurname.Text.Trim() + " " + clientName.Text.Trim();
        string[] ClientNameSurnameID = DropDownList1.SelectedValue.Split(' ');
        string ClientSurName = ClientNameSurnameID[0] + " " + ClientNameSurnameID[1];
         ClientID = Convert.ToInt32(ClientNameSurnameID[3]);
         VenueID = Convert.ToInt32(ClientNameSurnameID[2]);
        if (ClientSurName.ToLower() != TextFieldNameSurname.ToLower())
        {
            success = 0;
            userLbl.Visible = true;
            //RequiredFieldValidator8.Visible = false;
            userLbl.Text = "The Selected Venue belongs to: " + ClientSurName;
            string StartDte = SDate.Text;
            SDate.Text = StartDte;
            string EndtDte = EDate.Text;
            EDate.Text = EndtDte;
        }
        else
        {
            if (ValidateDatTime())
            {
                SdateLbl.Text = "";
            
                success = 1;
             
            }

        }

        return success;
    }

    private bool ValidateDatTime()
    {
        bool StartDateGreater = true;
        IBTCWebService ibtc = new IBTCWebService();
        List<courseAttr> sortedListCourses = ibtc.LoadVenues().OrderBy(x => x._EdateTime).ToList<courseAttr>();

        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
       // int clientID;
        string[] CourseNames = DropDownList1.SelectedValue.Split(' ');
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
            DateTime Sdate = Convert.ToDateTime(item._SdateTime.Trim());
            DateTime Edate = Convert.ToDateTime(item._EdateTime.Trim());

            int compare = DateTime.Compare(Sdate1, Edate);
            int compare2 = DateTime.Compare(Edate1, Sdate);

            if (clientName.Text.Trim() == item._name.Trim() && clientsurname.Text.Trim() == item._surname.Trim())
            {
                SdateLbl.Text = "";
                //if (CourseName.Trim() == item.courseName)
                //{
                if (DropDownList1.SelectedItem.Text.Trim() == item._venue.Trim())
                {

                    if (compare2 <= 0 || compare >= 0)
                    {
                        StartDateGreater = true;
                       
                    }
                    else
                    {
                        SdateLbl.Text = "There is an Event scheduled inbetween this Date Time.";
                        sdtlbl.Visible = false;
                        SdateLbl.Visible = true;
                        StartDateGreater = false;
                        string StartDte = SDate.Text;
                        SDate.Text = StartDte;
                        string EndtDte = EDate.Text;
                        EDate.Text = EndtDte;
                        break;
                      
                    }
                }
                //}
            }
        }
        return StartDateGreater;
    }

    protected int InsertBookMaker(int clientid, string TypeOfBooking, int VenueID)
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


        insert.Parameters.AddWithValue("@Name", Bookiename.Text.Trim());
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", Bookiesurname.Text.Trim());
        insert.Parameters.AddWithValue("@Email", Bookieemail.Text.Trim());
        insert.Parameters.AddWithValue("@Fax_no", BookieFax.Text.Trim());
        insert.Parameters.AddWithValue("@Tel_no", BookieFax.Text.Trim());
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);

        try
        {
            con.Close();
            con.Open();

            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
                lastID = Convert.ToInt32(id.Value);

                BookCourseMethod(clientid, lastID, VenueID);

                //else
                //{
                //    BookCourseMethod(clientid, lastID);
                //}
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
//            Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return lastID;
    }

    protected void BookCourseMethod(int ClientID, int BoomakerID, int VenueID)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCCourse(Visible, SDate,EDate,NumOfAttendees,VenueID,ClientID,BookMakerID,Time_stamp,EventID) values(@Visible,@SDate,@EDate,@NumOfAttendees,@ClientID,@VenueID,@BookMakerID,@timestamp,@EventID)", con);


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
                //clientemailLbl.Text = "";
                //lblCell.Text = "";
                //lblFax.Text = "";
                //lblTel.Text = "";
                DropDownList2.SelectedIndex = 0;
                DropDownList1.SelectedIndex = 0;
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
                userLbl.Text = "";
                Label1.Text = "";
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
                attendees.Text = "";
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


//private string getVenueName(int clientid)
//{
//    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
//    SqlCommand selectVenue = new SqlCommand("SELECT type from IBTCVenue where clientID = @clientID", con);

//    selectVenue.Parameters.AddWithValue("@clientID", clientid);

//    con.Close();
//    con.Open();
//    DataTable dt = new DataTable();
//    SqlDataAdapter sqa = new SqlDataAdapter(selectVenue);
//    sqa.Fill(dt);

//    return dt.Rows[0][0].ToString();

//}



