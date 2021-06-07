using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tuning.Models;

namespace tuning.Controllers
{
    public class BasvuruController : Controller
    {
        private hpchiptuningDBEntities2 db = new hpchiptuningDBEntities2();

        // GET: Basvuru
        public ActionResult Index()
        {
            return View(db.BasvuruSayfasis.ToList());
        }

        // GET: Basvuru/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasvuruSayfasi basvuruSayfasi = db.BasvuruSayfasis.Find(id);
            if (basvuruSayfasi == null)
            {
                return HttpNotFound();
            }
            return View(basvuruSayfasi);
        }

        // GET: Basvuru/Create
        public ActionResult HumanResources()
        {
            return View();
        }

        // POST: Basvuru/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HumanResources([Bind(Include = "Id,Ad,Soyad,Telefon,Email,PostaKodu,Sehir,Mahalle,Apartman,Tanıtım,Cv")] BasvuruSayfasi basvuruSayfasi)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(Request.Files[0].FileName);
                var path = Path.Combine(Server.MapPath("~/FormFiles/"), fileName);
                Request.Files[0].SaveAs(path);
                basvuruSayfasi.Cv = "~/FormFiles/" + fileName;
                db.BasvuruSayfasis.Add(basvuruSayfasi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(basvuruSayfasi);
        }

        // GET: Basvuru/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasvuruSayfasi basvuruSayfasi = db.BasvuruSayfasis.Find(id);
            if (basvuruSayfasi == null)
            {
                return HttpNotFound();
            }
            return View(basvuruSayfasi);
        }

        // POST: Basvuru/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,Soyad,Telefon,Email,PostaKodu,Sehir,Mahalle,Apartman,Tanıtım,Cv")] BasvuruSayfasi basvuruSayfasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basvuruSayfasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(basvuruSayfasi);
        }

        // GET: Basvuru/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasvuruSayfasi basvuruSayfasi = db.BasvuruSayfasis.Find(id);
            if (basvuruSayfasi == null)
            {
                return HttpNotFound();
            }
            return View(basvuruSayfasi);
        }

        // POST: Basvuru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasvuruSayfasi basvuruSayfasi = db.BasvuruSayfasis.Find(id);
            db.BasvuruSayfasis.Remove(basvuruSayfasi);
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
