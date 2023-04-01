using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GamerzList.MainPage
{
    public partial class AddGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                getAwards();

            }
              


        }
        public void getAwards()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                
                con.Open();
                string qry = "select award_name from awards_table";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    awardswon.Items.Add(sdr["award_name"].ToString()); 
                    con.Close();
                   

                }
                else
                {
                    errormessage.InnerHtml = "Could not find the content ";
                   
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                errormessage.InnerHtml = "You tried";

            }
            
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            int awardIdi = 0;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
            {
                connection.Open();
                var sql = "INSERT INTO Game_Table(gameTitle, gameImage, dateofRelease, awards_id, gameDescription) VALUES(@gameTitle, @gameImage, @dateofRelease, @awards_id, @gameDescription)";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    string awardId = checkAwards(awardswon.SelectedItem.Text);

                    if (awardId == " ")
                    {
                        awardIdi = 0;
                    }
                    else
                    {
                        awardIdi = (int)Convert.ToInt64(awardId);
                    }

                    // Convert the image to byte[]
                    byte[] bytes = null;
                    if (FileUpload1.HasFile)
                    {
                        using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                        {
                            bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength);
                        }
                    }

                    cmd.Parameters.AddWithValue("@gameTitle", GameTitle.Text);
                    cmd.Parameters.AddWithValue("@gameImage", bytes ?? (object)DBNull.Value); // Use DBNull.Value if bytes is null
                    cmd.Parameters.AddWithValue("@dateofRelease", Calendar1.SelectedDate.ToString());
                    cmd.Parameters.AddWithValue("@gameDescription", gDescription.Text);
                    if (awardIdi == 0)
                    {
                        cmd.Parameters.AddWithValue("@awards_id", SqlString.Null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@awards_id", awardIdi);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            Response.Redirect("../MainPage/MainPage.aspx");

            /* int awardIdi = 0; 
             using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
             {

                 connection.Open();
                 var sql = "INSERT INTO Game_Table(gameTitle, gameImage, dateofRelease, awards_id, gameDescription) VALUES(@gameTitle, @gameImage, @dateofRelease, @awards_id, @gameDescription)";
                 using (var cmd = new SqlCommand(sql, connection))
                 {
                     string awardId = checkAwards(awardswon.SelectedItem.Text); 

                     if(awardId == " ")
                     {
                         awardIdi = 0;
                     }
                     else
                     {
                         awardIdi = (int)Convert.ToInt64(awardId);
                     }


                     byte[] bytes;
                     string fileName = FileUpload1.FileName; 
                     using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                     {
                         bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength);
                     }

                     cmd.Parameters.AddWithValue("@gameTitle", GameTitle.Text);
                     cmd.Parameters.AddWithValue("@gameImage", bytes);
                     cmd.Parameters.AddWithValue("@dateofRelease", Calendar1.SelectedDate.ToString());
                     cmd.Parameters.AddWithValue("@gameDescription", gDescription.Text); 
                     if(awardIdi == 0)
                     {
                         cmd.Parameters.AddWithValue("@awards_id", SqlString.Null);
                     }
                     else
                     {
                         cmd.Parameters.AddWithValue("@awards_id", awardIdi);
                     }

                     cmd.ExecuteNonQuery();


                 }
             }
             Response.Redirect("../MainPage/MainPage.aspx");*/

        }

        protected void dateCustVal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Calendar1.SelectedDate == null
            || Calendar1.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        } 
        public string checkAwards(string awardName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                string awardID;
                con.Open();
                string qry = "select award_id from awards_table where award_name='" + awardName + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                { 
                    awardID = sdr[0].ToString();
                    con.Close();
                    return awardID;

                }
                else
                {
                    errormessage.InnerHtml = "Could not find the account ";
                    return " "; 
                    con.Close();
                }

                
            }
            catch (Exception ex)
            {
                errormessage.InnerHtml = "You tried";
                
            }
            return " ";
        }
    }
    }
