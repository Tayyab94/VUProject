using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vu_Project.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string WebUrl { get; set; }

        public int Category_Id { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }


        public DateTime PostDate { get; set; }

        public bool Approaved { get; set; }

        [ForeignKey("Category_Id")]
        public virtual Category  Category { get; set; }

        public virtual List<PostVisit> PostVisits { get; set; }
    }
}