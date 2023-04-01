using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GamerzList.UserPage
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
        }
        public string logIn(string uId, string uPass)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                
                con.Open();
                string qry = "select UserPFP from login where UserId='" + uId + "' and userPass='" + uPass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    byte[] bytes = (byte[])sdr["UserPFP"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    return "data:image/png;base64," + base64String;
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
    }
}