using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace My_Blog.Areas.Admin.Models
{
    public class PostMaintainViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}