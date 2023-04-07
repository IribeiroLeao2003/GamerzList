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
using System.Data;

namespace GamerzList.MainPage
{
    public partial class MainPage : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPostsFromDatabase();
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
           
            
            
        }

        protected void PostRepeater_ItemCommand(object sender, CommandEventArgs e)
        {
            int postId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Like")
            {
                UpdatePostLikes(postId, 1);
            }
            else if (e.CommandName == "Dislike")
            {
                UpdatePostLikes(postId, -1);
            }

            LoadPostsFromDatabase();
        }



        public byte[] GetUserProfileImage(string uId, string uPass)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                con.Open();
                byte[] data;
                string qry = "select UserPFP from login where UserName='" + uId + "' and userPass='" + uPass + "'";
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
                string qry = "select UserPFP from login where UserName='" + uId + "' and userPass='" + uPass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    byte[] data = (byte[])sdr["UserPFP"];

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
                string query = "SELECT p.Id, p.Title, p.Content, p.UserId, p.CreatedAt, p.Likes, p.Dislikes, COUNT(c.Id) as CommentsCount " +
                               "FROM dbo.Posts p LEFT JOIN dbo.Comments c ON p.Id = c.PostId " +
                               "GROUP BY p.Id, p.Title, p.Content, p.UserId, p.CreatedAt, p.Likes, p.Dislikes " +
                               "ORDER BY p.CreatedAt DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                PostRepeater.DataSource = sdr;
                PostRepeater.DataBind();
                con.Close();
            }
        }


        private void SavePostToDatabase(string postContent, string postTitle, string userId)
        {
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
            string postTitle = txtPostTitle.Text.Trim();
            string postContent = txtPostContent.Text.Trim();

            if (!string.IsNullOrEmpty(postTitle) && !string.IsNullOrEmpty(postContent))
            {
                string userId = Session["Username"].ToString();
                SavePostToDatabase(postContent, postTitle, userId);

                LoadPostsFromDatabase();

                
                txtPostTitle.Text = string.Empty;
                txtPostContent.Text = string.Empty;
            }
            else
            {
                LoadPostsFromDatabase();
            }
        }
   
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
                        DateCreated = (DateTime)sdr["DateCreated"],
                        Likes = (int)sdr["Likes"],
                        Dislikes = (int)sdr["Dislikes"]
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
                string qry = "select UserPFP from login where UserName='" + uId + "' and userPass='" + uPass + "'";
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
        private void UpdatePostLikes(int postId, int likeValue)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
            {
                con.Open();
                string qry = "UPDATE Posts SET Likes = Likes + @LikeValue, Dislikes = Dislikes - @DislikeValue WHERE Id = @PostId";
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    if (likeValue > 0)
                    {
                        cmd.Parameters.AddWithValue("@LikeValue", 1);
                        cmd.Parameters.AddWithValue("@DislikeValue", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LikeValue", 0);
                        cmd.Parameters.AddWithValue("@DislikeValue", 1);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int postId = Convert.ToInt32(btn.CommandArgument);
            UpdatePostLikes(postId, 1);
            LoadPostsFromDatabase();
        }

        protected void btnDislike_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int postId = Convert.ToInt32(btn.CommandArgument);
            UpdatePostLikes(postId, -1);
            LoadPostsFromDatabase();
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