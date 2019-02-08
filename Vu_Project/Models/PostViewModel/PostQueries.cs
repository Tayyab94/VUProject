using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vu_Project.Models.PostViewModel
{
    public class PostQueries
    {
        private ApplicationDbContext _context = new ApplicationDbContext();



        public Post GetPostByID(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return _context.Posts.Where(x => x.Id == id).Include(x => x.Category).SingleOrDefault();
            }
        }  
        

        public void UpdatePost(Post obj)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                  _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();

                

            }
        }

        public List<Post> GetAllPostsList()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {

                return _context.Posts.Include(a => a.Category).Take(12).OrderByDescending(i=>i.Id).ToList();
               
            }
        }
    }

}