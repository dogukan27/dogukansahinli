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
    public class IletisimController : Controller
    {
        private hpchiptuningDBEntities2 db = new hpchiptuningDBEntities2();

        // GET: Iletisim
        public ActionResult Index()
        {
            return View(db.iletisimSayfasis.ToList());
        }

        // GET: Iletisim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iletisimSayfasi iletisimSayfasi = db.iletisimSayfasis.Find(id);
            if (iletisimSayfasi == null)
            {
                return HttpNotFound();
            }
            return View(iletisimSayfasi);
        }

        // GET: Iletisim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Iletisim/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,baslik,adres,email,telefon,map")] iletisimSayfasi iletisimSayfasi)
        {
            if (ModelState.IsValid)
            {
                db.iletisimSayfasis.Add(iletisimSayfasi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iletisimSayfasi);
        }

        // GET: Iletisim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iletisimSayfasi iletisimSayfasi = db.iletisimSayfasis.Find(id);
            if (iletisimSayfasi == null)
            {
                return HttpNotFound();
            }
            return View(iletisimSayfasi);
        }

        // POST: Iletisim/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,baslik,adres,email,telefon,map")] iletisimSayfasi iletisimSayfasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iletisimSayfasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisimSayfasi);
        }

        // GET: Iletisim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iletisimSayfasi iletisimSayfasi = db.iletisimSayfasis.Find(id);
            if (iletisimSayfasi == null)
            {
                return HttpNotFound();
            }
            return View(iletisimSayfasi);
        }

        // POST: Iletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            iletisimSayfasi iletisimSayfasi = db.iletisimSayfasis.Find(id);
            db.iletisimSayfasis.Remove(iletisimSayfasi);
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
