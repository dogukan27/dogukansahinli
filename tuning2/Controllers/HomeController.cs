using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuning.Models;
using tuning;
using tuning.ViewModels;
using System.IO;
using System.Data.Entity;

namespace tuning.Controllers
{
    
    public class HomeController : Controller
    {
        private hpchiptuningDBEntities2 dBEntities = new hpchiptuningDBEntities2();
        // GET: Home
        public ActionResult Index()
        {
            var homeModel = new HomeModel() { 
                Sliders= dBEntities.sliders.ToList(),
                Markalar = dBEntities.ArabaMarkas.ToList(),
                Modeller = dBEntities.ArabaModels.ToList(),
                Motorlar = dBEntities.ArabaMotors.ToList(),
                Yillar = dBEntities.ArabaYils.ToList(),
                Tuningliaraclars=dBEntities.tuningliaraclars.ToList()
            };


            return View(homeModel);
        }

        [HttpGet]
        public ActionResult Hesapla()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Hesapla(FormCollection form)
        {
            var motorAdi = form["motoradi"];
            double yil = double.Parse(form["yil"]);
            double model= double.Parse(form["model"]);
            double yakit= double.Parse(form["yakit"]);
            ViewBag.marka= form["marka"];

            var araba = dBEntities.ArabaMotors.Where(i => i.MotorAdi == motorAdi).Where(i=>i.ModelID==model&&i.YilID==yil&&i.YakitTip==yakit).FirstOrDefault();
            return View(araba);
        }

        public ActionResult About()
        {
            var model = new AboutModel()
            {
                Misyon = dBEntities.misyonumuzSayfasis.ToList(),
                Vizyon = dBEntities.vizyonumuzSayfasis.ToList(),
                Hakkinda = dBEntities.hakkimizdaSayfasis.ToList(),
                SssSayfasis = dBEntities.sssSayfasis.ToList(),
                IletisimSayfasi = dBEntities.iletisimSayfasis.ToList()

            };
            return View(model);
        }

        public ActionResult EditAbout()
        {
            var model = new AboutModel()
            {
                Misyon=dBEntities.misyonumuzSayfasis.ToList(),
                Vizyon=dBEntities.vizyonumuzSayfasis.ToList(),
                Hakkinda=dBEntities.hakkimizdaSayfasis.ToList()

            };
            return View(model);
        }

        public ActionResult EditMisyon(int id)
        {
            
            var misyon=dBEntities.misyonumuzSayfasis.Find(id);

            return View(misyon);
        }

        [HttpPost]
        public ActionResult EditMisyon(misyonumuzSayfasi misyonumuz)
        {
            if (ModelState.IsValid)
            {
                dBEntities.Entry(misyonumuz).State = EntityState.Modified;
                dBEntities.SaveChanges();
                return RedirectToAction("EditAbout", "Home");

            }
            return View(misyonumuz);
        }

        public ActionResult EditVizyon(int id)
        {
            var vizyon = dBEntities.vizyonumuzSayfasis.Find(id);

            return View(vizyon);
        }


        [HttpPost]
        public ActionResult EditVizyon(vizyonumuzSayfasi vizyonumuz)
        {
            if (ModelState.IsValid)
            {
                dBEntities.Entry(vizyonumuz).State = EntityState.Modified;
                dBEntities.SaveChanges();
                return RedirectToAction("EditAbout", "Home");

            }
            return View(vizyonumuz);
        }


        public ActionResult EditHak(int id)
        {
            var hakkında = dBEntities.hakkimizdaSayfasis.Find(id);

            return View(hakkında);
        }


        [HttpPost]
        public ActionResult EditHak(hakkimizdaSayfasi hakkimizda)
        {
            if (ModelState.IsValid)
            {
                dBEntities.Entry(hakkimizda).State = EntityState.Modified;
                dBEntities.SaveChanges();
                return RedirectToAction("EditAbout", "Home");

            }
            return View(hakkimizda);
        }


        public ActionResult SSS()
        {
            var sss=dBEntities.sssSayfasis.ToList();
            return View(sss);
        }

        public ActionResult EditSSS(int id)
        {
            var sssedit = dBEntities.sssSayfasis.Find(id);
            return View(sssedit);
        }

        [HttpPost]

        public ActionResult EditSSS(sssSayfasi sayfasi)
        {
            if (ModelState.IsValid)
            {
                dBEntities.Entry(sayfasi).State = EntityState.Modified;
                dBEntities.SaveChanges();
                return RedirectToAction("SSS", "Home");

            }
            return View(sayfasi);
        }

        public ActionResult Faq()
        {
            hpchiptuningDBEntities2 dBEntities2 = new hpchiptuningDBEntities2();
            return View(dBEntities2.sorulars.ToList());
        }
    }
}