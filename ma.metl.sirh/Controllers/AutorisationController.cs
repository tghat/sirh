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
    public class AutorisationController : Controller
    {
        private sirhContext db = new sirhContext();

        // GET: /Autorisation/
        public ActionResult Index()
        {
            var autorisations = db.Autorisations.Include(a => a.Module);
            return View(autorisations.ToList());
        }

        // GET: /Autorisation/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorisation autorisation = db.Autorisations.Find(id);
            if (autorisation == null)
            {
                return HttpNotFound();
            }
            return View(autorisation);
        }

        // GET: /Autorisation/Create
        public ActionResult Create()
        {
            ViewBag.Module_Id = new SelectList(db.Modules, "Id", "Code");
            return View();
        }

        // POST: /Autorisation/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Code,Description,Module_Id,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Autorisation autorisation)
        {
            if (ModelState.IsValid)
            {
                db.Autorisations.Add(autorisation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Module_Id = new SelectList(db.Modules, "Id", "Code", autorisation.Module_Id);
            return View(autorisation);
        }

        // GET: /Autorisation/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorisation autorisation = db.Autorisations.Find(id);
            if (autorisation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Module_Id = new SelectList(db.Modules, "Id", "Code", autorisation.Module_Id);
            return View(autorisation);
        }

        // POST: /Autorisation/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Code,Description,Module_Id,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Autorisation autorisation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorisation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Module_Id = new SelectList(db.Modules, "Id", "Code", autorisation.Module_Id);
            return View(autorisation);
        }

        // GET: /Autorisation/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorisation autorisation = db.Autorisations.Find(id);
            if (autorisation == null)
            {
                return HttpNotFound();
            }
            return View(autorisation);
        }

        // POST: /Autorisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Autorisation autorisation = db.Autorisations.Find(id);
            db.Autorisations.Remove(autorisation);
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
