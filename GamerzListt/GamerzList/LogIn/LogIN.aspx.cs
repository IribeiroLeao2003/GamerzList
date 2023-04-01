using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Drawing;
using System.Configuration;
using System.Reflection.Emit;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace GamerzList
{
    public partial class LogIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()); 
                
                string uName = UserName.Text;
                string uPass = UserPassword.Text;
                con.Open();
                string qry = "select * from login where UserId='" + uName + "' and userPass='" + uPass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["UserName"] = uName;
                    Session["UserPass"] = uPass;
                    con.Close();
                    Response.Redirect("../MainPage/MainPage.aspx");
                }
                else
                {
                    errormessage.InnerHtml = "Could not find the account ";

                }
                con.Close();
            
            }
            catch (Exception ex)
            {
                errormessage.InnerHtml = "You tried";
            }
            
        }

        protected void StayUser1_Click(object sender, EventArgs e)
        {
            Session["UserName"] = "User";
            Session["UserPass"] = "nothing"; 
            Response.Redirect("../MainPage/MainPage.aspx");
        }
    }
}