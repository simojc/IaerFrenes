using System;
using System.Collections.Generic;
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
    public class OrgController : Controller
    {
        private arbredb db = new arbredb();

        void ChargerToutesLesDDL()
        {
            var w_profilUtil = db.prof_utils
                  .Select(s => new SelectListItem
                  {
                      Value = s.id.ToString(),
                      Text = s.nom + " " + s.pren
                  }).ToList();

            ViewBag.profilUtil = new SelectList(w_profilUtil, "Value", "Text");

            var w_localisation = db.localisations
                 .Select(s => new SelectListItem
                 {
                     Value = s.id.ToString(),
                     Text = s.emplcmt + " - " + s.code_post + " - " + s.num_civc + " - " + s.nom_rue + " - " + s.ville
                 }).ToList();

            ViewBag.localisation = new SelectList(w_localisation, "Value", "Text");

            var w_org = db.organisations
              .Select(s => new SelectListItem
              {
                  Value = s.id.ToString(),
                  Text = s.nom + " - " + s.sigle + " - " 
              }).ToList();

            ViewBag.org = new SelectList(w_org, "Value", "Text");
        }

        // GET: Org
        public async Task<ActionResult> Index()
        {
            return View(await db.organisations.ToListAsync());
        }

        // GET: Org/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            org org = await db.organisations.FindAsync(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            return View(org);
        }

        // GET: Org/Create
        public ActionResult Create()
        {
            ChargerToutesLesDDL();

            return View();
        }

        // POST: Org/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,niv,nom,sigle,parnt,cntct,adr,util,dt_cretn,dt_modf")] org org)
        {
            if (ModelState.IsValid)
            {
                org.util = User.Identity.GetUserName();
                db.organisations.Add(org);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ChargerToutesLesDDL();
            return View(org);
        }

        // GET: Org/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            org org = await db.organisations.FindAsync(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            ChargerToutesLesDDL();
            return View(org);
        }

        // POST: Org/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,niv,nom,sigle,parnt,cntct,adr,util,dt_cretn,dt_modf")] org org)
        {
            if (ModelState.IsValid)
            {
                org.util = User.Identity.GetUserName();
                db.Entry(org).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ChargerToutesLesDDL();
            return View(org);
        }

        // GET: Org/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            org org = await db.organisations.FindAsync(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            return View(org);
        }

        // POST: Org/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            org org = await db.organisations.FindAsync(id);
            db.organisations.Remove(org);
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
