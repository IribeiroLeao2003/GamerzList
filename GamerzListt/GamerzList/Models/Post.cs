using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamerzList.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}