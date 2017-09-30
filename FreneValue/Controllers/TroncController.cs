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
    public class TroncController : Controller
    {
        private arbredb db = new arbredb();

        // GET: troncs
        public async Task<ActionResult> Index()
        {
            return View(await db.troncs.ToListAsync());
        }

        // GET: troncs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tronc tronc = await db.troncs.FindAsync(id);
            if (tronc == null)
            {
                return HttpNotFound();
            }
            return View(tronc);
        }

        // GET: troncs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: troncs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_eval,no_tronc,id_tronc_parnt,dhp,diam_moy,haut_moy,morphlg,racdmt,qual,cavt,fent_fissre,blesr,contaminatn,sympt_visuel,possede_cime,est_branch_maitr,long_moy,catgr_branch_maitr,nb_branch_maitr,comm,util,dt_cretn,dt_modf")] tronc tronc)
        {
            if (ModelState.IsValid)
            {
                db.troncs.Add(tronc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tronc);
        }

        // GET: troncs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tronc tronc = await db.troncs.FindAsync(id);
            if (tronc == null)
            {
                return HttpNotFound();
            }
            return View(tronc);
        }

        // POST: troncs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,id_eval,no_tronc,id_tronc_parnt,dhp,diam_moy,haut_moy,morphlg,racdmt,qual,cavt,fent_fissre,blesr,contaminatn,sympt_visuel,possede_cime,est_branch_maitr,long_moy,catgr_branch_maitr,nb_branch_maitr,comm,util,dt_cretn,dt_modf")] tronc tronc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tronc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tronc);
        }

        // GET: troncs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tronc tronc = await db.troncs.FindAsync(id);
            if (tronc == null)
            {
                return HttpNotFound();
            }
            return View(tronc);
        }

        // POST: troncs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tronc tronc = await db.troncs.FindAsync(id);
            db.troncs.Remove(tronc);
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
