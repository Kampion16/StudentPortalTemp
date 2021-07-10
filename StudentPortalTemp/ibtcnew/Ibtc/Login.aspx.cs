using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
                    
        SqlCommand selectUser = new SqlCommand("select username from IBTCuser where username = @username", con);
        SqlCommand selectPass = new SqlCommand("select password from IBTCuser where username = @password", con);
        //string username = Request.Form["txtName"];
        //string password = Request.Form["txtPassword"];

        selectUser.Parameters.AddWithValue("@username", txtName.Text);
        selectPass.Parameters.AddWithValue("@password", txtPassword.Text);
        int succes = 0;
       

        try
        {
            con.Close();
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sqda = new SqlDataAdapter(selectUser);
            sqda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                try
                {                    
                    
                        dt = new DataTable();
                        sqda = new SqlDataAdapter(selectPass);
                        sqda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                        Session["username"] = txtName.Text;
                        Response.Redirect("Default - copy.aspx");
                    }
                    else
                    {

                        errmsg.Text = "Password is incorrect!";
                    }
                                
                }
                catch (Exception)
                {

                    errmsg.Text = "Password is incorrect!";
                }
               
                
            }
            else
            {
                errmsg.Text = "Username does not exist!";
            }
        }
        catch
        {

            errmsg.Text = "Username does not exist!";
        }

        finally
        {
            con.Close();
        }
        }
    }
}