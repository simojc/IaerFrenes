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
    public class ProfilUtilController : Controller
    {
        private arbredb db = new arbredb();

        private ApplicationDbContext context = new ApplicationDbContext();

        void ChargerToutesLesDDL()
        {              
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
              
            var w_uti = context.Users
             .Select(s => new SelectListItem
             {
                 Value = s.Id .ToString(),
                 Text = s.UserName
                 //Text = s.UserName + " - " + s.Address + " - "
             }).ToList();

            ViewBag.w_uti = new SelectList(w_uti, "Value", "Text");
        }


        // GET: ProfilUtil
        public async Task<ActionResult> Index()
        {
            return View(await db.prof_utils.ToListAsync());
        }

        // GET: ProfilUtil/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prof_util prof_util = await db.prof_utils.FindAsync(id);
            if (prof_util == null)
            {
                return HttpNotFound();
            }
            return View(prof_util);
        }

        // GET: ProfilUtil/Create
        public ActionResult Create()
        {
            ChargerToutesLesDDL();
            return View();
        }

        // POST: ProfilUtil/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_util,typ_util,typ_propr,nom,pren,org,ss_org,telph,extnsn,tel_cell,id_local,util,dt_cretn,dt_modf")] prof_util prof_util)
        {
            if (ModelState.IsValid)
            {
                prof_util.util = User.Identity.GetUserName();
                db.prof_utils.Add(prof_util);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ChargerToutesLesDDL();

            return View(prof_util);
        }

        // GET: ProfilUtil/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prof_util prof_util = await db.prof_utils.FindAsync(id);
            if (prof_util == null)
            {
                return HttpNotFound();
            }
            ChargerToutesLesDDL();
            return View(prof_util);
        }

        // POST: ProfilUtil/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,id_util,typ_util,typ_propr,nom,pren,org,ss_org,telph,extnsn,tel_cell,id_local,util,dt_cretn,dt_modf")] prof_util prof_util)
        {
            if (ModelState.IsValid)
            {
                prof_util.util = User.Identity.GetUserName();
                db.Entry(prof_util).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ChargerToutesLesDDL();
            return View(prof_util);
        }

        // GET: ProfilUtil/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prof_util prof_util = await db.prof_utils.FindAsync(id);
            if (prof_util == null)
            {
                return HttpNotFound();
            }
            return View(prof_util);
        }

        // POST: ProfilUtil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            prof_util prof_util = await db.prof_utils.FindAsync(id);
            db.prof_utils.Remove(prof_util);
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
