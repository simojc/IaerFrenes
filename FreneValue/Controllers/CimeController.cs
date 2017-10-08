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
    public class CimeController : Controller
    {
        private arbredb db = new arbredb();

        // GET: Cime
        public async Task<ActionResult> Index()
        {
            return View(await db.cimes.ToListAsync());
        }

        // GET: Cime/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cime cime = await db.cimes.FindAsync(id);
            if (cime == null)
            {
                return HttpNotFound();
            }
            return View(cime);
        }

        // GET: Cime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cime/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_tronc,racdmt,dens_brach,dens_feuille,dens_rameaux,sympt_visuel,defaut,intfrce,travaux,util,dt_cretn,dt_modf")] cime cime)
        {
            if (ModelState.IsValid)
            {
                cime.util = User.Identity.GetUserName();
                db.cimes.Add(cime);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cime);
        }

        // GET: Cime/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cime cime = await db.cimes.FindAsync(id);
            if (cime == null)
            {
                return HttpNotFound();
            }
            return View(cime);
        }

        // POST: Cime/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,id_tronc,racdmt,dens_brach,dens_feuille,dens_rameaux,sympt_visuel,defaut,intfrce,travaux,util,dt_cretn,dt_modf")] cime cime)
        {
            if (ModelState.IsValid)
            {
                cime.util = User.Identity.GetUserName();
                db.Entry(cime).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cime);
        }

        // GET: Cime/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cime cime = await db.cimes.FindAsync(id);
            if (cime == null)
            {
                return HttpNotFound();
            }
            return View(cime);
        }

        // POST: Cime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            cime cime = await db.cimes.FindAsync(id);
            db.cimes.Remove(cime);
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
