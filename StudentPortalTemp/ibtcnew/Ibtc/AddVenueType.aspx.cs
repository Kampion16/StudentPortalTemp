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

public partial class AddEventType : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["username"] = EventName.Text;
    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        if (CheckEventExists())
        {

        }
        else
        {
            CheckBoomaker();
        }
      
       
    }

    private bool CheckEventExists()
    {
        EventLBL.Visible = false;
        vname.Visible = true;
        bool eventExist = false;
        SqlCommand insert = new SqlCommand("Select VenueName from IBTCVenueTypes where VenueName = @VenueName", con);

        insert.Parameters.Add("@VenueName", EventName.Text.Trim());

        con.Close();
        con.Open();

        SqlDataReader sdr = insert.ExecuteReader();

        if (sdr.HasRows)
        {
            eventExist = true;
            EventLBL.Text = "Venue already exists!";
            EventLBL.Visible = true;
            vname.Visible = false;
        }

        return eventExist;
    }

    protected void CheckBoomaker()
    {
        BookieemailLbl.Text = "";
        BookieemailLbl.Visible = false;
        BookieemailLbl.Text = "";
        BookieFaxLbl.Text = "";
        BookieFaxLbl.Visible = false;
        BookiecellLbl.Text = "";
        BookiecellLbl.Visible = false;
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
            BookiecellLbl.Text = "Fax No. format is incorrect.  e.g. 0123365556";
            RequiredFieldValidator13.Visible = false;
            BookiecellLbl.Visible = true;
        }

        if (matchFax.Success && match.Success && matchMail.Success)
        {
            InsertBookMaker();
        }
        
    }
    protected int InsertBookMaker()
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
                Bookiename.Text = "";
                Bookiesurname.Text = "";
                Bookieemail.Text = "";
                BookieFax.Text = "";
                Bookiecell.Text = "";
                BookieemailLbl.Text = "";
                BookieFaxLbl.Text = "";
                BookiecellLbl.Text = "";


                InsertVenue(lastID);
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('ERROR 101 : Please Contact Web Administator');", true);
         //   Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
        }

        finally
        {
            con.Close();
        }

        return lastID;
    }

    protected void InsertVenue(int BookmakerID)
    {
        SqlCommand insert = new SqlCommand("insert into IBTCVenueTypes(VenueName, VenueDes, BookmakerID) values(@VenueName,@VenueDes,@BookmakerID)", con);

        //SqlParameter id = new SqlParameter("id", 0);
        //id.Direction = ParameterDirection.Output;
        //int lastID = 0;
        int success = 0;

        //string Name = Request.Form["BookMakername"];
        //string Surname = Request.Form["BookMakerSurname"];
        //string email = Request.Form["BookMakerEmail"];
        //string Fax = Request.Form["BookMakerFaxNo"];
        //string Tell = Request.Form["BookMakerTellNo"];


        insert.Parameters.AddWithValue("@VenueName", EventName.Text);
        insert.Parameters.AddWithValue("@VenueDes", EventDes.Text);
       
       

        try
        {
            con.Close();
            con.Open();
            insert.Parameters.Add("@BookmakerID", BookmakerID);
            success = insert.ExecuteNonQuery();
            if (success == 1)
            {
               // lastID = Convert.ToInt32(id.Value);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('The Venue has been succesfully added');", true);
               // Response.Write("<script>alert('The Venue has been succesfully added')</script>");
                EventName.Text = "";
                EventDes.Text = "";
                Bookiename.Text = "";
                    Bookiesurname.Text = "";
                        Bookieemail.Text = "";
                            BookieFax.Text = "";
                            Bookiecell.Text = "";
                            vname.Visible = true;
                            RequiredFieldValidator6.Visible = true;
                            RequiredFieldValidator12.Visible = true;
                            RequiredFieldValidator13.Visible = true;

            //    InsertBookMaker(clientid, lastID, VenueID);

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

      //  return lastID;
    }
}