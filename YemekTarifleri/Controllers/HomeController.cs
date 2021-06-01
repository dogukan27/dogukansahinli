using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekTarifleri.Models;

namespace YemekTarifleri.Controllers
{
    public class HomeController : Controller
    {
        private DataContext context = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var yemekler = context.Foods.Where(i => i.IsApproved == true && i.IsHome == true).Include(i=>i.Category).ToList();
            return View(yemekler);
        }
    }
}