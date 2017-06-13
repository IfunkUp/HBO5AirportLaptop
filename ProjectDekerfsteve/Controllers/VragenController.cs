using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDekerfsteve;

namespace ProjectDekerfsteve.Controllers
{
    public class VragenController : Controller
    {
        private INFO_c1035462Entities db = new INFO_c1035462Entities();

        // GET: Vragen
        public ActionResult Index()
        {
            return View(db.Proj_Vragen.ToList());
        }

        // GET: Vragen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragen vragen = db.Proj_Vragen.Find(id);
            if (vragen == null)
            {
                return HttpNotFound();
            }
            return View(vragen);
        }

        // GET: Vragen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vragen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,naam,voornaam,adres,gemeente,type,content,date")] Vragen vragen)
        {
            if (ModelState.IsValid)
            {
                db.Proj_Vragen.Add(vragen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vragen);
        }

        // GET: Vragen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragen vragen = db.Proj_Vragen.Find(id);
            if (vragen == null)
            {
                return HttpNotFound();
            }
            return View(vragen);
        }

        // POST: Vragen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,naam,voornaam,adres,gemeente,type,content,date")] Vragen vragen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vragen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vragen);
        }

        // GET: Vragen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragen vragen = db.Proj_Vragen.Find(id);
            if (vragen == null)
            {
                return HttpNotFound();
            }
            return View(vragen);
        }

        // POST: Vragen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vragen vragen = db.Proj_Vragen.Find(id);
            db.Proj_Vragen.Remove(vragen);
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
