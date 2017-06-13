using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectDekerfsteve;

namespace ProjectDekerfsteve.Controllers
{
    public class evenementController : Controller
    {
        private INFO_c1035462Entities db = new INFO_c1035462Entities();

        // GET: evenement
        public ActionResult Index()
        {
            //var proj_evenementen = db.Proj_evenementen.Include(e => e.gemeente);
            var evenementen = db.Proj_evenementen.Where(x => x.datum >= DateTime.Now).GroupBy(x => x.datum.Month).ToList();
            return View(evenementen);
        }

        // GET: evenement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenement evenement = db.Proj_evenementen.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            var test = db.proj_inschrijvingen.Where(x => x.evenement_id == evenement.id).Count();
            if (test <= 0)
            {
                ViewBag.aantalIngeschreven = 0;
            }
            else
            {
                ViewBag.aantalIngeschreven = db.proj_inschrijvingen.Where(x => x.evenement_id == evenement.id)
                    .Sum(x => x.aantal_personen);
            }
            
            return View(evenement);
        }

        [Authorize( Roles = "Admin, Organisator")]
        public ActionResult Create()
        {
            ViewBag.locatie = new SelectList(db.Proj_gemeenten, "id", "naam");
            return View();
        }

        // POST: evenement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Organisator")]
        public ActionResult Create([Bind(Include = "id,naam,beschrijving,locatie,datum,Max_inschrijvingen")] evenement evenement)
        {
            if (ModelState.IsValid)
            {
                db.Proj_evenementen.Add(evenement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.locatie = new SelectList(db.Proj_gemeenten, "id", "naam", evenement.locatie);
            return View(evenement);
        }

        [Authorize(Roles = "Admin, Organisator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenement evenement = db.Proj_evenementen.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            ViewBag.locatie = new SelectList(db.Proj_gemeenten, "id", "naam", evenement.locatie);
            return View(evenement);
        }

        // POST: evenement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Organisator")]
        public ActionResult Edit([Bind(Include = "id,naam,beschrijving,locatie,datum")] evenement evenement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.locatie = new SelectList(db.Proj_gemeenten, "id", "naam", evenement.locatie);
            return View(evenement);
        }

        // GET: evenement/Delete/5
        [Authorize(Roles = "Admin, Organisator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenement evenement = db.Proj_evenementen.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: evenement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Organisator")]
        public ActionResult DeleteConfirmed(int id)
        {
            evenement evenement = db.Proj_evenementen.Find(id);
            db.Proj_evenementen.Remove(evenement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Subscribe(evenement e)
        {
            ViewBag.evenement = e;

            return View();
        }
        [HttpPost]
        public ActionResult Subscribe(evenement e, int aantal)
        {

            db.proj_inschrijvingen.Add(new inschrijving
            {
                aantal_personen = aantal,
                evenement_id = e.id,
                persoon_id = User.Identity.GetUserId()
            });
            db.SaveChangesAsync();

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
