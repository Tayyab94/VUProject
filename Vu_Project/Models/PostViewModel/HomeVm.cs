using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vu_Project.Models.PostViewModel
{
    public class HomeVm
    {
        public List<Post>  Posts { get; set; }

        public ICollection<Category> CategoriesList { get; set; }
    }
}