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
    public class TuningliController : Controller
    {
        private hpchiptuningDBEntities2 db = new hpchiptuningDBEntities2();

        // GET: Tuningli
        public ActionResult Index()
        {
            return View(db.tuningliaraclars.ToList());
        }

        // GET: Tuningli/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tuningliaraclar tuningliaraclar = db.tuningliaraclars.Find(id);
            if (tuningliaraclar == null)
            {
                return HttpNotFound();
            }
            return View(tuningliaraclar);
        }

        // GET: Tuningli/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tuningli/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,marka,model,orjbeygir,orjtork,kbeygir,ktork,resim,yolu,yakit")] tuningliaraclar tuningliaraclar)
        {
            if (ModelState.IsValid)
            {
                db.tuningliaraclars.Add(tuningliaraclar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tuningliaraclar);
        }

        // GET: Tuningli/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tuningliaraclar tuningliaraclar = db.tuningliaraclars.Find(id);
            if (tuningliaraclar == null)
            {
                return HttpNotFound();
            }
            return View(tuningliaraclar);
        }

        // POST: Tuningli/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,marka,model,orjbeygir,orjtork,kbeygir,ktork,resim,yolu,yakit")] tuningliaraclar tuningliaraclar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuningliaraclar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuningliaraclar);
        }

        // GET: Tuningli/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tuningliaraclar tuningliaraclar = db.tuningliaraclars.Find(id);
            if (tuningliaraclar == null)
            {
                return HttpNotFound();
            }
            return View(tuningliaraclar);
        }

        // POST: Tuningli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tuningliaraclar tuningliaraclar = db.tuningliaraclars.Find(id);
            db.tuningliaraclars.Remove(tuningliaraclar);
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
