using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vu_Project.Models.PostViewModel
{

    public class PostVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string WebUrl { get; set; }

        [Display(Name = "Category Id")]
        public int Category_Id { get; set; }

        [Required]
        public string Description { get; set; }


        public List<Category> CategoryList { get; set; }
    }
    public class EditPostVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]

        public string WebUrl { get; set; }

        public int Category_Id { get; set; }

        [Required]
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public List<Category> CategoryList { get; set; }
    }

    public class PostGalleryViewModel
    {
        public int MaximumPrice { get; set; }

        public string searchITem { get; set; }
        public List<Post> postList { get; set; }

        public int? SortBy { get; set; }

        public int? cataId { get; set; }
        public List<Category> FeaturedCatagories { get; set; }

        public Pager pager { get; set; }
    }
}