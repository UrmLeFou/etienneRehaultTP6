﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Dojo.Data;

namespace Dojo.Controllers
{
    public class ArmesController : Controller
    {
        private Context db = new Context();

        // GET: Armes
        public ActionResult Index()
        {
            return View(db.Armes.ToList());
        }

        // GET: Armes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arme arme = db.Armes.Find(id);
            if (arme == null)
            {
                return HttpNotFound();
            }
            return View(arme);
        }

        // GET: Armes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Armes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Id,Nom,Degats")]*/ Arme arme)
        {
            if (ModelState.IsValid)
            {
                db.Armes.Add(arme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arme);
        }

        // GET: Armes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arme arme = db.Armes.Find(id);
            if (arme == null)
            {
                return HttpNotFound();
            }
            return View(arme);
        }

        // POST: Armes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "Id,Nom,Degats")]*/ Arme arme)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(arme).State = EntityState.Modified;
                var armeDb = db.Armes.Find(arme.Id);
                armeDb.Nom = arme.Nom;
                armeDb.Degats = arme.Degats;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arme);
        }

        // GET: Armes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arme arme = db.Armes.Find(id);
            if (arme == null)
            {
                return HttpNotFound();
            }
            return View(arme);
        }

        // POST: Armes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arme arme = db.Armes.Find(id);
            db.Armes.Remove(arme);
            var samouraisListe = db.Samourais.Where(s => s.Arme.Id == arme.Id);
            foreach (var samourai in samouraisListe)
            {
                samourai.Arme = null;
            }
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
