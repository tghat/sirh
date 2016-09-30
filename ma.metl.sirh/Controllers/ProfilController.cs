using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ma.metl.sirh.Model;
using ma.metl.sirh.Service;

namespace ma.metl.sirh.Controllers
{
    public class ProfilController : Controller
    {
        IProfilService profilService; 

        public ProfilController(IProfilService profilService)
        {
            this.profilService = profilService; 
        }

        // GET: /Profil/
        public ActionResult Index()
        {
            return View(profilService.GetAll());
        }

        // GET: /Profil/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = profilService.GetById(Convert.ToInt32(id)); 
                
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // GET: /Profil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Profil/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Code,Libelle,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Profil profil)
        {
            if (ModelState.IsValid)
            {
                profilService.Create(profil);
                return RedirectToAction("Index");
            }

            return View(profil);
        }

        // GET: /Profil/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = profilService.GetById(Convert.ToInt32(id));
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // POST: /Profil/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Code,Libelle,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Profil profil)
        {
            if (ModelState.IsValid)
            {
                
                //db.Entry(profil).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profil);
        }

        // GET: /Profil/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = profilService.GetById(Convert.ToInt32(id)); 
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // POST: /Profil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Profil profil = profilService.GetById(Convert.ToInt32(id));
            profilService.Delete(profil); 
            return RedirectToAction("Index");
        }

    }
}
