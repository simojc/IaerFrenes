using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreneValue.Models;

namespace FreneValue.Controllers
{
    public class SoucheController : Controller
    {
        private arbredb db = new arbredb();

        // GET: souches
        public async Task<ActionResult> Index()
        {
            return View(await db.souches.ToListAsync());
        }

        // GET: souches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            souche souche = await db.souches.FindAsync(id);
            if (souche == null)
            {
                return HttpNotFound();
            }
            return View(souche);
        }

        // GET: souches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: souches/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_eval,dhs,acces_nutrmt,aeration_sol,surf_deplmt_racin,racine_hs,blesre_racine,cavite_hrs_sol,exig_essouchmt,typ_essouchmt,profdeur_essouchmt,ray_rognage,defaut,infrstr,specificite,haut_souche,exig_abat,espace_subs,fosse_plant,comm,util,dt_cretn,dt_modf")] souche souche)
        {
            if (ModelState.IsValid)
            {
                db.souches.Add(souche);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(souche);
        }

        // GET: souches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            souche souche = await db.souches.FindAsync(id);
            if (souche == null)
            {
                return HttpNotFound();
            }
            return View(souche);
        }

        // POST: souches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,id_eval,dhs,acces_nutrmt,aeration_sol,surf_deplmt_racin,racine_hs,blesre_racine,cavite_hrs_sol,exig_essouchmt,typ_essouchmt,profdeur_essouchmt,ray_rognage,defaut,infrstr,specificite,haut_souche,exig_abat,espace_subs,fosse_plant,comm,util,dt_cretn,dt_modf")] souche souche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(souche).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(souche);
        }

        // GET: souches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            souche souche = await db.souches.FindAsync(id);
            if (souche == null)
            {
                return HttpNotFound();
            }
            return View(souche);
        }

        // POST: souches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            souche souche = await db.souches.FindAsync(id);
            db.souches.Remove(souche);
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
