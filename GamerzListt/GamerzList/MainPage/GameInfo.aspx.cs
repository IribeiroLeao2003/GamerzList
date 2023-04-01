using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GamerzList.MainPage
{
    public partial class GameInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Session["UserName"].ToString();
            string passWord = Session["UserPass"].ToString();
            if (userName != "User")
            {
                accountName.InnerHtml = userName;
                byte[] myImage = logIn(userName, passWord);
                if (myImage == null)
                {
                    userPfp.ImageUrl = "../Images/efda444fa90a377715eef7239c5bc291.png";
                }
                string imageData = Convert.ToBase64String(myImage);

                userPfp.ImageUrl = "data:image/jpeg;base64," + imageData;

            }  
            else
            {
                accountName.InnerHtml = "User";
                userPfp.ImageUrl = "../Images/efda444fa90a377715eef7239c5bc291.png";
            }
        }
        public byte[] logIn(string uId, string uPass)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());


                con.Open();
                byte[] data;
                string qry = "select UserPFP from login where UserId='" + uId + "' and userPass='" + uPass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    data = (byte[])sdr["UserPFP"];
                    return data;

                }
                else
                {
                    return null;

                }
                con.Close();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Session["UserPass"] = null;
            Response.Redirect("../Login/LogIN.aspx");
        }
    }
}