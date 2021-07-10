using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BookedVenues()
    {

    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

        //GridView2.Columns[6].Visible = false;
        //GridView2.Columns[7].Visible = false;
        GridView2.Columns[8].Visible = false;
        GridView2.Columns[9].Visible = false;
        GridView2.Columns[10].Visible = false;
        GridView2.Columns[11].Visible = false;


    }
    protected void GridView2_DataBound(object sender, EventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //   {
        //           GridView2.Columns[2].Visible = false;
        //       }
        //   }

        //if (GridView2.EditIndex > -1)
        //{
        //    GridView2.Columns[4].Visible = false;
        //    GridView2.Columns[5].Visible = false;
        //    GridView2.Columns[6].Visible = false;
        //    GridView2.Columns[7].Visible = false;
        //    GridView2.Columns[8].Visible = false;
        //}

        //   // GridView2.Columns[5].Visible = false;
        //else {
        //    GridView2.Columns[4].Visible = true;
        //    GridView2.Columns[5].Visible = true;
        //    GridView2.Columns[6].Visible = true;
        //    GridView2.Columns[7].Visible = true;
        //    GridView2.Columns[8].Visible = true;
        //    //GridView2.Columns[5].Visible = true;
        //}

    }

    protected void CourseBookSub(object sender, EventArgs e)
    {
        string ftype = "course";
        BookClient(ftype);

    }

    protected void BookClient(string TypeOfBooking)
    {
        SqlCommand insert = new SqlCommand("insert into IBTClient(Name, Surname, Phone_Number,Email,Fax_no,Tel_no,Time_stamp) values(@Name, @Surname, @Phone_Number,@Email,@Fax_no,@Tel_no,@timestamp); select @id=SCOPE_IDENTITY()", con);

        string ClientName = Request.Form["name"];
        string ClientSurname = Request.Form["Surname"];
        string Tell_Number = Request.Form["Tell"];
        string Email = Request.Form["email"];
        string Cell = Request.Form["Cell"];
        string Fax_no = Request.Form["Fax"];
        int succes = 0;
        //string Sdate = Request.Form["Sdate"];
        //string EDate = Request.Form["EDate"];

        SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;


        insert.Parameters.AddWithValue("@Name", ClientName);
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", ClientSurname);
        insert.Parameters.AddWithValue("@Email", Email);
        insert.Parameters.AddWithValue("@Phone_Number", Cell);
        insert.Parameters.AddWithValue("@Tel_no", Tell_Number);
        insert.Parameters.AddWithValue("@Fax_no", Fax_no);
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

            Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

    }


    protected int InsertBookMaker(int clientid, string TypeOfBooking)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCBookMaker(Name, Surname,Email,Fax_no,Tel_no,Time_stamp) values(@Name,@Surname,@Email,@Fax_no,@Tel_no,@timestamp);select @id=SCOPE_IDENTITY()", con);

        SqlParameter id = new SqlParameter("id", 0);
        id.Direction = ParameterDirection.Output;
        int lastID = 0;
        int success = 0;

        string Name = Request.Form["BookMakername"];
        string Surname = Request.Form["BookMakerSurname"];
        string email = Request.Form["BookMakerEmail"];
        string Fax = Request.Form["BookMakerFaxNo"];
        string Tell = Request.Form["BookMakerTellNo"];


        insert.Parameters.AddWithValue("@Name", Name);
        insert.Parameters.Add(id);
        insert.Parameters.AddWithValue("@Surname", Surname);
        insert.Parameters.AddWithValue("@Email", email);
        insert.Parameters.AddWithValue("@Fax_no", Fax);
        insert.Parameters.AddWithValue("@Tel_no", Tell);
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);

        try
        {
            con.Close();
            con.Open();

            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
                lastID = Convert.ToInt32(id.Value);
                if (TypeOfBooking == "Venue")
                {
                    // BookVenueMethod(clientid, lastID);
                }
                else
                {
                    BookCourseMethod(clientid, lastID);
                }
            }


        }
        catch
        {

            Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return lastID;
    }



    protected void BookCourseMethod(int ClientID, int BoomakerID)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCCourse(Title, SDate,EDate,NumOfAttendees,ClientID,BookMakerID,Time_stamp) values(@Title, @SDate,@EDate,@NumOfAttendees,@ClientID,@BookMakerID,@timestamp)", con);



        string Title = Request.Form["DropDownList1"];
        string Sdate = Request.Form["SDate"];
        string EDate = Request.Form["EDate"];
        string NumOfAttendees = Request.Form["attendees"];


        insert.Parameters.AddWithValue("@BookMakerID", BoomakerID);
        insert.Parameters.AddWithValue("@Title", Title);
        insert.Parameters.AddWithValue("@NumOfAttendees", NumOfAttendees);
        insert.Parameters.AddWithValue("@SDate", Sdate);
        insert.Parameters.AddWithValue("@EDate", EDate);
        insert.Parameters.AddWithValue("@ClientID", ClientID);
        insert.Parameters.AddWithValue("@timestamp", DateTime.Now);


        try
        {

            con.Close();
            con.Open();
            int succes = insert.ExecuteNonQuery();
            if (succes == 1)
            {
                Response.Write("<script>alert('The Course has been succesfully booked')</script>");
            }
        }
        catch
        {

            Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }
    }


    protected void vwCourse_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        GridView2.Visible = true;
        //string getLbl = Request.Form["vnb"];
        vnb.Text = "Event Schedules";

    }

    protected void vwVenue_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        GridView2.Visible = false;
        vnb.Text = "Venue Bookings";
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        attendeesLbl.Visible = false;
        attendeesLbl.Text = "";
        attendeesLbl.Visible = false;
        //SDateLbl2.Visible = true;
        attendeesLbl.Visible = false;
        SDateLbl.Visible = false;
        GridView2.Columns[6].Visible = true;
        GridView2.Columns[7].Visible = true;
        GridView2.Columns[8].Visible = true;
        GridView2.Columns[9].Visible = true;
        GridView2.Columns[10].Visible = true;
        GridView2.Columns[11].Visible = true;
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        attendeesLbl.Visible = false;
        attendeesLbl.Text = "";
        attendeesLbl.Visible = false;
        //SDateLbl2.Visible = true;
        attendeesLbl.Visible = false;
        SDateLbl.Visible = false;
        nameLbl.Visible = false; ;

        TextBox Attendees = (TextBox)GridView2.Rows[e.RowIndex].FindControl("Textbox2");
        TextBox Sdate = (TextBox)GridView2.Rows[e.RowIndex].FindControl("_SdateTime");
        Label lblAttenees = (Label)GridView2.Rows[e.RowIndex].FindControl("attendeesLbl2");
        DateTime SDateOutput;
        TextBox name = (TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox1");
        TextBox surname = (TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox4");

        bool ParsSdate = DateTime.TryParseExact(Sdate.Text,"yyyy/MM/dd HH:mm:ss",CultureInfo.InvariantCulture,DateTimeStyles.None, out SDateOutput);

        if (!ValidateNameSurname(name.Text, surname.Text))
        {
            nameLbl.Text = "The user - '"+name.Text+"' '"+surname.Text+"' has not booked a Venue.";
            nameLbl.Visible = true;
            e.Cancel = true;
            return;
        } 

        if (Attendees.Text == "0")
        {            
            //&#8226            
            attendeesLbl.Text = " Attendees must be greater than 0";
            attendeesLbl.Visible = true;
            e.Cancel = true;
            return;
        }
        else if (!ParsSdate)
        {
            SDateLbl.Text = "Incorrect Date Format e.g. yyyy-MM-dd HH:mm:ss";
            
            //SdateLbl2.Text = "*";
            //SdateLbl2.Visible = true;
            SDateLbl.Visible = true;
            e.Cancel = true;
            return;
        }
        else
        {              
            Response.Write("<script>alert('The Course has been succesfully Updated')</script>");
            GridView2.Columns[6].Visible = true;
            GridView2.Columns[7].Visible = true;
            GridView2.Columns[8].Visible = true;
            GridView2.Columns[9].Visible = true;
            GridView2.Columns[10].Visible = true;
            GridView2.Columns[11].Visible = true;
        }  
        }

    private bool ValidateNameSurname(string name, string surname)
    {
        bool validated = false;
        IBTCWebService tbc = new IBTCWebService();
        bool success = tbc.UserHasVenue(name,surname);

        if (success)
        {
            validated = true;
        }
        else
        {
            validated = false;
        }
        return validated;
    }
    protected void AddEntry_Click(object sender, EventArgs e)
    {

    }
}
