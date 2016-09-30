using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ma.metl.sirh.Model;


namespace ma.metl.sirh.Controllers
{
    public class AutorisationProfilController : Controller
    {
        private sirhContext db = new sirhContext();

        // GET: /AutorisationProfil/
        public ActionResult Index()
        {
            var autorisationprofils = db.AutorisationProfils.Include(a => a.Autorisation).Include(a => a.Profil);
            return View(autorisationprofils.ToList());
        }

        // GET: /AutorisationProfil/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutorisationProfil autorisationprofil = db.AutorisationProfils.Find(id);
            if (autorisationprofil == null)
            {
                return HttpNotFound();
            }
            return View(autorisationprofil);
        }

        // GET: /AutorisationProfil/Create
        public ActionResult Create()
        {
            ViewBag.Autorisation_Id = new SelectList(db.Autorisations, "Id", "Code");
            ViewBag.Profil_Id = new SelectList(db.Profils, "Id", "Code");
            return View();
        }

        // POST: /AutorisationProfil/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Autorisation_Id,Profil_Id,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] AutorisationProfil autorisationprofil)
        {
            if (ModelState.IsValid)
            {
                db.AutorisationProfils.Add(autorisationprofil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Autorisation_Id = new SelectList(db.Autorisations, "Id", "Code", autorisationprofil.Autorisation_Id);
            ViewBag.Profil_Id = new SelectList(db.Profils, "Id", "Code", autorisationprofil.Profil_Id);
            return View(autorisationprofil);
        }

        // GET: /AutorisationProfil/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutorisationProfil autorisationprofil = db.AutorisationProfils.Find(id);
            if (autorisationprofil == null)
            {
                return HttpNotFound();
            }
            ViewBag.Autorisation_Id = new SelectList(db.Autorisations, "Id", "Code", autorisationprofil.Autorisation_Id);
            ViewBag.Profil_Id = new SelectList(db.Profils, "Id", "Code", autorisationprofil.Profil_Id);
            return View(autorisationprofil);
        }

        // POST: /AutorisationProfil/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Autorisation_Id,Profil_Id,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] AutorisationProfil autorisationprofil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorisationprofil).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Autorisation_Id = new SelectList(db.Autorisations, "Id", "Code", autorisationprofil.Autorisation_Id);
            ViewBag.Profil_Id = new SelectList(db.Profils, "Id", "Code", autorisationprofil.Profil_Id);
            return View(autorisationprofil);
        }

        // GET: /AutorisationProfil/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutorisationProfil autorisationprofil = db.AutorisationProfils.Find(id);
            if (autorisationprofil == null)
            {
                return HttpNotFound();
            }
            return View(autorisationprofil);
        }

        // POST: /AutorisationProfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AutorisationProfil autorisationprofil = db.AutorisationProfils.Find(id);
            db.AutorisationProfils.Remove(autorisationprofil);
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
