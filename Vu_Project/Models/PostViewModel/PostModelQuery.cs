using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vu_Project.Models.PostViewModel
{
    public class PostModelQuery
    {
       
        public List<Post> GetShowAllPost(string sreachProduct, int? cataId, int? SortBy, int pageNo, int pageSize)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                //  List<Post> postsList = _context.Posts.Include(x => x.Category).OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                List<Post> postsList = _context.Posts.Include(x => x.Category).OrderByDescending(x => x.Id).ToList();


                if (cataId.HasValue)
                {
                    postsList = postsList.Where(x => x.Category_Id == cataId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(sreachProduct))
                {
                    postsList = postsList.Where(x => x.Name.ToLower().Contains(sreachProduct.ToLower())
                    
                    || x.Category.Name.ToLower().Contains(sreachProduct.ToLower())).ToList();
                }


                //return postsList.ToList();
                return postsList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}