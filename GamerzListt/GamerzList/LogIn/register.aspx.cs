using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Drawing;
using Image = System.Drawing.Image;

namespace GamerzList
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

            var email = new EmailAddressAttribute();

            if (email.IsValid(uEmail.Text) == false)
            {
                errormessage.InnerHtml = "Email invalid";
            }
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
            {

                connection.Open();
                var sql = "INSERT INTO Login(UserId, FirstName, LastName, userEmail, UserPass, userPFP) VALUES(@UserId, @FirstName, @LastName, @userEmail, @userPass, @userPFP )";
                using (var cmd = new SqlCommand(sql, connection))
                {


                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                    {
                        bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength);
                    }

                    cmd.Parameters.AddWithValue("@UserId", UserID.Text);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName.Text);
                    cmd.Parameters.AddWithValue("@Lastname", LastName.Text);
                    cmd.Parameters.AddWithValue("@userEmail", uEmail.Text);
                    cmd.Parameters.AddWithValue("@userPass", uPassw.Text);
                    cmd.Parameters.AddWithValue("@userPFP", bytes);
                    cmd.ExecuteNonQuery();

                }
            }
            Session["UserName"] = UserID.Text;
            Session["UserPass"] = uPassw.Text;
            Response.Redirect("../MainPage/MainPage.aspx");
        }
    }
}
    
