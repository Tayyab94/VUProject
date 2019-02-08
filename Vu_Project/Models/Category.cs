using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vu_Project.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    
        public string ImgUrl { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}