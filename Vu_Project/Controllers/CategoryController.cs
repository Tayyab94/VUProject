using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vu_Project.Models;

namespace Vu_Project.Controllers
{



    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [Authorize(Roles = VU.Admin)]
        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = VU.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                Category obj = new Category();

                obj.Name = category.Name;

                long uno = DateTime.Now.Ticks;

                int count = 0;

                foreach (string file_Name in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[file_Name];

                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string url = "/Images/CategoryImage/" + $"{uno}_{++count}" + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        string path = Server.MapPath(url);

                        file.SaveAs(path);

                        obj.ImgUrl = url;
                    }

                }
                db.Categories.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        [Authorize(Roles = VU.Admin)]
        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = VU.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                long uno = DateTime.Now.Ticks;

                int count = 0;

                foreach (string file_Name in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[file_Name];

                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        if (!string.IsNullOrEmpty(category.ImgUrl))
                        {
                            if (System.IO.File.Exists(Request.MapPath("~/" + category.ImgUrl)))
                            {
                                System.IO.File.Delete(Request.MapPath("~/" + category.ImgUrl));
                            }
                        }

                        string url = "/Images/PostImage/" + $"{uno}_{++count}" + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        string path = Server.MapPath(url);

                        file.SaveAs(path);

                        category.ImgUrl = url;
                    }
                    else
                    {
                        category.ImgUrl = category.ImgUrl;
                    }

                }

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);

            if (!string.IsNullOrEmpty(category.ImgUrl))
            {
                if (System.IO.File.Exists(Request.MapPath("~/" + category.ImgUrl)))
                {
                    System.IO.File.Delete(Request.MapPath("~/" + category.ImgUrl));
                }
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DetailCategory(int? id)
        {
            var category = db.Categories.Include(c => c.Posts).Where(x => x.Id == id);
           // category.SingleOrDefault()
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
