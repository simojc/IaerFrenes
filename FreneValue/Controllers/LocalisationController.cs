using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FreneValue.Models;

namespace FreneValue.Controllers
{
    public class LocalisationController : Controller
    {
        private arbredb db = new arbredb();

        // GET: Localisation
        public async Task<ActionResult> Index()
        {
            return View(await db.localisations.ToListAsync());
        }

        // GET: Localisation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loclsn loclsn = await db.localisations.FindAsync(id);
            if (loclsn == null)
            {
                return HttpNotFound();
            }
            return View(loclsn);
        }

        // GET: Localisation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localisation/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,num_civc,voie,lot,nom_rue,tronc_rue,nom_cours_eau,sectn_cours_eau,emplcmt,code_post,ville,prov,pays,suprfc,lattd,longtd,util,dt_cretn,dt_modf")] loclsn loclsn)
        {
            if (ModelState.IsValid)
            {
                loclsn.util = User.Identity.GetUserName();
                db.localisations.Add(loclsn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loclsn);
        }

        // GET: Localisation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loclsn loclsn = await db.localisations.FindAsync(id);
            if (loclsn == null)
            {
                return HttpNotFound();
            }
            return View(loclsn);
        }

        // POST: Localisation/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,num_civc,voie,lot,nom_rue,tronc_rue,nom_cours_eau,sectn_cours_eau,emplcmt,code_post,ville,prov,pays,suprfc,lattd,longtd,util,dt_cretn,dt_modf")] loclsn loclsn)
        {
            if (ModelState.IsValid)
            {
                loclsn.util = User.Identity.GetUserName();
                db.Entry(loclsn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loclsn);
        }

        // GET: Localisation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loclsn loclsn = await db.localisations.FindAsync(id);
            if (loclsn == null)
            {
                return HttpNotFound();
            }
            return View(loclsn);
        }

        // POST: Localisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            loclsn loclsn = await db.localisations.FindAsync(id);
            db.localisations.Remove(loclsn);
            await db.SaveChangesAsync();
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
