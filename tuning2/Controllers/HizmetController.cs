using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuning.Models;
using tuning.ViewModels;

namespace tuning.Controllers
{
    public class HizmetController : Controller
    {
        private hpchiptuningDBEntities2 db = new hpchiptuningDBEntities2();
        // GET: Hizmet
        public ActionResult Index()
        {
            var model = new HizmetModel() { 
                BenzinChip=db.benzinchipuygulamasis.ToList(),
                Box=db.boxuygulamasis.ToList(),
                ChipYazilim=db.chipyazilimiuygulamasis.ToList(),
                Pedal=db.pedaluygulamasis.ToList(),
                Tork=db.torkvegucartisis.ToList()
            };
            return View(model);
        }

        public ActionResult EditBenzinChip(int id)
        {
            var benzin = db.benzinchipuygulamasis.Find(id);

            return View(benzin);
        }

        [HttpPost]
        public ActionResult EditBenzinChip(benzinchipuygulamasi benzinchipuygulamasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benzinchipuygulamasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Hizmet");

            }
            return View(benzinchipuygulamasi);
        }

        public ActionResult EditBox(int id)
        {
            var box = db.boxuygulamasis.Find(id);

            return View(box);
        }

        [HttpPost]
        public ActionResult EditBox(boxuygulamasi boxuygulamasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boxuygulamasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Hizmet");

            }
            return View(boxuygulamasi);
        }

        public ActionResult EditChip(int id) {

            var chip = db.chipyazilimiuygulamasis.Find(id);

            return View(chip);
        }


        [HttpPost]
        public ActionResult EditChip(chipyazilimiuygulamasi chipyazilimiuygulamasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chipyazilimiuygulamasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Hizmet");

            }
            return View(chipyazilimiuygulamasi);
        }

        public ActionResult EditPedal(int id)
        {
            var pedal = db.pedaluygulamasis.Find(id);

            return View(pedal);
        }

        [HttpPost]
        public ActionResult EditPedal(pedaluygulamasi pedaluygulamasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedaluygulamasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Hizmet");

            }
            return View(pedaluygulamasi);
        }

        public ActionResult EditTork(int id)
        {

            var tork = db.torkvegucartisis.Find(id);

            return View(tork);
        }



        [HttpPost]
        public ActionResult EditTork(torkvegucartisi torkvegucartisi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(torkvegucartisi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Hizmet");

            }
            return View(torkvegucartisi);
        }

        public ActionResult Service()
        {
            var model = new HizmetModel()
            {
                BenzinChip = db.benzinchipuygulamasis.ToList(),
                Box = db.boxuygulamasis.ToList(),
                ChipYazilim = db.chipyazilimiuygulamasis.ToList(),
                Pedal = db.pedaluygulamasis.ToList(),
                Tork = db.torkvegucartisis.ToList()
            };
            return View(model);
        }
    }
}