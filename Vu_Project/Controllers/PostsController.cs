using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vu_Project.Models;
using Vu_Project.Models.PostViewModel;

namespace Vu_Project.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = VU.Admin)]  //hahahahahahha
        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category);
            return View(posts.ToList());

        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        [Authorize]
        // GET: Posts/Create
        public ActionResult Create()
        {
           
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostVM post, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                Post obj = new Post();
                obj.Name = post.Name;


                if (post.WebUrl.Contains("http") || post.WebUrl.Contains("https"))
                {
                    string[] te = new string[] { "http", "https", ":", "//" };   
                    string[] tay = post.WebUrl.Split(te, StringSplitOptions.None);
                    obj.WebUrl = tay[3];

                }
                else
                {
                    obj.WebUrl = post.WebUrl;

                }
                obj.Category_Id = post.Category_Id;

                obj.Description = post.Description;
                obj.PostDate = DateTime.Now;
                obj.Approaved = true;

                long uno = DateTime.Now.Ticks;

                int count = 0;

                foreach (string file_Name in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[file_Name];

                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string url = "/Images/PostImage/" + $"{uno}_{++count}" + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        string path = Server.MapPath(url);

                        file.SaveAs(path);

                        obj.ImgUrl = url;
                    }

                }
                db.Posts.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", post.Category_Id);
            //ViewBag.Cat = new SelectList(db.Categories, "Id", "Name", post.Category_Id);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            EditPostVM obj = new EditPostVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = new PostQueries().GetPostByID(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            obj.Id = post.Id;
            obj.Name = post.Name;

            obj.WebUrl = post.WebUrl;

            obj.ImgUrl = post.ImgUrl;
            obj.Description = post.Description;

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", post.Category_Id);
            return View(obj);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostVM obj)
        {
            if (ModelState.IsValid)
            {


                /*Post postobj = new Post();*/
                var postobj = new PostQueries().GetPostByID(obj.Id);

                postobj.Id = obj.Id;
                postobj.Name = obj.Name;
                if (obj.WebUrl.Contains("http") || obj.WebUrl.Contains("https"))
                {
                    string[] te = new string[] { "http", "https", ":", "//" };
                    string[] tay = obj.WebUrl.Split(te, StringSplitOptions.None);
                    postobj.WebUrl = tay[4];

                }
                else
                {
                    postobj.WebUrl = obj.WebUrl;

                }
                postobj.WebUrl = obj.WebUrl;
                postobj.Category = null;
                postobj.Category_Id = obj.Category_Id;
                postobj.Description = obj.Description;
                postobj.PostDate = DateTime.Now;
                postobj.Approaved = true;
                //postobj.ImgUrl = obj.ImgUrl;
                long uno = DateTime.Now.Ticks;

                int count = 0;

                foreach (string file_Name in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[file_Name];

                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        if (!string.IsNullOrEmpty(postobj.ImgUrl))
                        {
                            if (System.IO.File.Exists(Request.MapPath("~/" + postobj.ImgUrl)))
                            {
                                System.IO.File.Delete(Request.MapPath("~/" + postobj.ImgUrl));
                            }
                        }

                        string url = "/Images/PostImage/" + $"{uno}_{++count}" + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        string path = Server.MapPath(url);

                        file.SaveAs(path);

                        postobj.ImgUrl = url;
                    }
                    else
                    {
                        postobj.ImgUrl = postobj.ImgUrl;
                    }

                }



                PostQueries queries = new PostQueries();
                queries.UpdatePost(postobj);




                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", obj.Category_Id);
            return View(obj);
        }

        public ActionResult Delete(int id)
        {

            var post = db.Posts.Find(id);

            if (!string.IsNullOrEmpty(post.ImgUrl))
            {
                if (System.IO.File.Exists(Request.MapPath("~/" + post.ImgUrl)))
                {
                    System.IO.File.Delete(Request.MapPath("~/" + post.ImgUrl));
                }
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ShowAllPosts(string sreachProduct, int? CataId, int? SortBy, int? pageNo)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            PostModelQuery postModel = new PostModelQuery();

            PostGalleryViewModel model = new PostGalleryViewModel();

            model.FeaturedCatagories = _context.Categories.ToList();


            model.searchITem = sreachProduct;
            if (SortBy.HasValue)
            {
                model.SortBy = SortBy.Value;
            }

            if (CataId.HasValue)
            {
                model.cataId = CataId.Value;
            }


            pageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;

            model.postList = postModel.GetShowAllPost(sreachProduct, CataId, SortBy, pageNo.Value, 4);

            var total = _context.Posts.Count();
            model.pager = new Pager(total, pageNo, 4);
            return View(model);
        }


        public JsonResult VisitWebSite(int id)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            Post p = _context.Posts.Find(id);
            int Pid = p.Id;

            string urlWeb = p.WebUrl;

            PostVisit postVisit = new PostVisit();
            postVisit.PostId = Pid;
            postVisit.VistTime = DateTime.Now;
            _context.PostVisits.Add(postVisit);
            _context.SaveChanges();

         
            return Json(urlWeb, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles =VU.Admin)]
        [HttpGet]

        public ActionResult Abs(string name)
        {
            DateTime d = DateTime.Now.AddMonths(-1);
            ApplicationDbContext context = new ApplicationDbContext();
            AbC ab = new AbC();
            try
            {
                var p = context.Posts.Where(x => x.Name.ToLower().Equals(name.ToLower())).SingleOrDefault();
                var WebPost = context.PostVisits.Include(x => x.Post).Where(x => x.PostId == p.Id && x.VistTime > d).Count();


                ab.post = p;
                ab.Count = WebPost;

                return View(ab);
            }
            catch
            {
                return View();
            }

        }



    }
}
