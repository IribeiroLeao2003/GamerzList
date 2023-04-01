using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Image = System.Drawing.Image;
using System.CodeDom;
using Microsoft.SqlServer.Server;
using static System.Net.Mime.MediaTypeNames;
using GamerzList.Models;

namespace GamerzList.MainPage
{
    public partial class MainPage : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPostsFromDatabase();
            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (Session["UserName"] != null && Session["UserPass"] != null)
            {
                string userName = Session["UserName"].ToString();
                string passWord = Session["UserPass"].ToString();

                if (userName != "User")
                {
                    byte[] myImage = GetUserProfileImage(userName, passWord);
                    if (myImage == null)
                    {
                        userPfp.Attributes["src"] = "../Images/efda444fa90a377715eef7239c5bc291.png";
                    }
                    else
                    {
                        string imageData = Convert.ToBase64String(myImage);
                        userPfp.Attributes["src"] = "data:image/jpeg;base64," + imageData;
                    }
                }
                else
                {
                    userPfp.Attributes["src"] = "../Images/efda444fa90a377715eef7239c5bc291.png";
                }
            }
            else
            {
                userPfp.Attributes["src"] = "../Images/efda444fa90a377715eef7239c5bc291.png";
            }
        }

        // ... (rest of the code)

        public byte[] GetUserProfileImage(string uId, string uPass)
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

        public bool LogIn(string uId, string uPass)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                con.Open();
                string qry = "select Id, UserPFP from login where UserId='" + uId + "' and userPass='" + uPass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    int id = (int)sdr["Id"];
                    byte[] data = (byte[])sdr["UserPFP"];

                    Session["UserId"] = id;
                    Session["UserName"] = uId;
                    Session["UserPass"] = uPass;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void LoadPostsFromDatabase()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
            {
                con.Open();
                string qry = "SELECT * FROM Posts ORDER BY CreatedAt DESC";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                PostRepeater.DataSource = sdr;
                PostRepeater.DataBind();
                con.Close();
            }
        }

        private void SavePostToDatabase(string postContent, string postTitle, string userId)
        {
            string postUserId = Session["UserId"].ToString(); // Check for null with ?. operator
            if (string.IsNullOrEmpty(userId))
            {
                // If the user is not logged in, redirect to the login page
                Response.Redirect("~/Login.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["mycon"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Posts (UserId, Title, Content, CreatedAt) VALUES (@UserId, @Title, @Content, @CreatedAt)";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Title", postTitle);
                    command.Parameters.AddWithValue("@Content", postContent);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }

        }

        protected void btnSubmitPost_Click(object sender, EventArgs e)
        {
            string postContent = txtPostContent.Text.Trim();
            string postTitle = "My Post Title"; // You can change this to a value entered by the user, or remove it altogether
            string userId = Session["UserId"] as string; // Use 'as' keyword to avoid NullReferenceException

            if (string.IsNullOrEmpty(userId))
            {
                // If the user is not logged in, redirect to the login page
                Response.Redirect("../Login/LogIN.aspx");
                return;
            }

            if (!string.IsNullOrEmpty(postContent))
            {
                SavePostToDatabase(postContent, postTitle, userId);
                Response.Redirect(Request.RawUrl); // Refresh the page
            }
        }
        /*
        public void getGameimg()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                con.Open();
                byte[] data;
                string qry = "select gameImage from Game_Table";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read() && currentGameLine <= 4) // use a loop instead of if statement
                {
                    data = (byte[])sdr["gameImage"];
                    if (data.Length > 0)
                    {
                        string imageData = Convert.ToBase64String(data);

                        switch (currentGameLine)
                        {
                            case 1:
                                GamesLine1.ImageUrl = "data:image/jpeg;base64," + imageData;
                                GamesLine1.Visible = true;
                                break;
                            case 2:
                                GamesLine2.ImageUrl = "data:image/jpeg;base64," + imageData;
                                GamesLine2.Visible = true;
                                break;
                            case 3:
                                GamesLine3.ImageUrl = "data:image/jpeg;base64," + imageData;
                                GamesLine3.Visible = true;
                                break;
                            case 4:
                                GamesLine4.ImageUrl = "data:image/jpeg;base64," + imageData;
                                GamesLine4.Visible = true;
                                break;
                        }

                        currentGameLine++;
                    }
                }

                con.Close();

            }
            catch (Exception ex)
            {
                // Handle exception
            }
        } 
        */
        public void LoadPosts()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
            {
                con.Open();
                string qry = "SELECT * FROM Posts ORDER BY DateCreated DESC";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Post post = new Post
                    {
                        Id = (int)sdr["Id"],
                        Title = sdr["Title"].ToString(),
                        Content = sdr["Content"].ToString(),
                        UserId = sdr["UserId"].ToString(),
                        DateCreated = (DateTime)sdr["DateCreated"]
                    };
                    posts.Add(post);
                }

                con.Close();
            }

            PostRepeater.DataSource = posts;
            PostRepeater.DataBind();
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

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            Response.Redirect("AddGame.aspx");
        }

        protected void Games_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("GameInfo.aspx");
        }

        protected void userPfp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../UserPage/UserInfo.aspx");
        }

        protected void Unnamed_Click2(object sender, EventArgs e)
        {
            Response.Redirect("AddGame.aspx");
        }

        protected void LogoutButtonNav_Click(object sender, EventArgs e)
        {
            Response.Redirect("../LogIn/LogIN.aspx");
        }
    } 
}