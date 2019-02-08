using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vu_Project.Models;
using Vu_Project.Models.PostViewModel;

namespace Vu_Project.Controllers
{
    public class HomeController : Controller
    {
     private  ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            HomeVm model = new HomeVm();

            model.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public PartialViewResult ShowPost()
        {
            HomeVm home = new HomeVm();
            home.Posts = new PostQueries().GetAllPostsList();

            return PartialView(home.Posts);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}