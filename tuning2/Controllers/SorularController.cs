using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tuning.Models;

namespace tuning.Controllers
{
    public class SorularController : Controller
    {
        private hpchiptuningDBEntities2 db = new hpchiptuningDBEntities2();

        // GET: Sorular
        public ActionResult Index()
        {
            return View(db.sorulars.ToList());
        }

        // GET: Sorular/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sorular sorular = db.sorulars.Find(id);
            if (sorular == null)
            {
                return HttpNotFound();
            }
            return View(sorular);
        }

        // GET: Sorular/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sorular/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,baslik,soru")] sorular sorular)
        {
            if (ModelState.IsValid)
            {
                db.sorulars.Add(sorular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sorular);
        }

        // GET: Sorular/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sorular sorular = db.sorulars.Find(id);
            if (sorular == null)
            {
                return HttpNotFound();
            }
            return View(sorular);
        }

        // POST: Sorular/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,baslik,soru")] sorular sorular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sorular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sorular);
        }

        // GET: Sorular/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sorular sorular = db.sorulars.Find(id);
            if (sorular == null)
            {
                return HttpNotFound();
            }
            return View(sorular);
        }

        // POST: Sorular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sorular sorular = db.sorulars.Find(id);
            db.sorulars.Remove(sorular);
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
    }
}
