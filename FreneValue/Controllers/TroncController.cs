
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FreneValue.Models;
using FreneValue.Infrastructure;

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

        void ChargerToutesLesDDLTronc()
        {
            ViewBag.MORPHOLOGIE = Utilitaires.LireCodeValeurCache("MORPHOLOGIE");
            ViewBag.CLASSE_HAUTEUR = Utilitaires.LireCodeValeurCache("CLASSE_HAUTEUR");
            ViewBag.SYMPT_VISUEL = Utilitaires.LireCodeValeurCache("SYMPT_VISUEL");
            ViewBag.CONTAMINATION = Utilitaires.LireCodeValeurCache("CONTAMINATION");
            ViewBag.QUALITE_VALO = Utilitaires.LireCodeValeurCache("QUALITE_VALO");
            ViewBag.CAVITE = Utilitaires.LireCodeValeurCache("CAVITE");
            ViewBag.FENTE_EXTERNE = Utilitaires.LireCodeValeurCache("FENTE_EXTERNE");
            ViewBag.BLESSURE = Utilitaires.LireCodeValeurCache("BLESSURE");
            ViewBag.CATEGORIE_BM = Utilitaires.LireCodeValeurCache("CATEGORIE_BM");

            ViewBag.RACCORDEMENT = Utilitaires.LireCodeValeurCache("RACCORDEMENT");
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
        public ActionResult Create(int id_eval)
        {
            if (id_eval == null)
            {
                return HttpNotFound();
            }

            var w_troncs = db.troncs
           .Where(s => s.id_eval == id_eval)
             .Select(s => new SelectListItem
             {
                 Value = s.id.ToString(),
                 Text = s.no_tronc.ToString()
             }).ToList();
            //  w_troncs.Add(new SelectListItem() { Value = String.Empty, Text = String.Empty });
            ViewBag.w_tronc = new SelectList(w_troncs, "Value", "Text");

            ChargerToutesLesDDLTronc();

            tronc model = new tronc();
            model.id_eval = id_eval;
            return View(model);
            // return View();
        }

        // POST: troncs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_eval,no_tronc,id_tronc_parnt,dhp,diam_moy,haut_moy,morphlg,racdmt,qual,cavt,fent_fissre,blesr,contaminatn,sympt_visuel,possede_cime,possede_bm,est_branch_maitr,long_moy,catgr_branch_maitr,nb_branch_maitr,comm,util,dt_cretn,dt_modf")] tronc tronc)
        {
            if (ModelState.IsValid)
            {
                tronc.util = User.Identity.GetUserName();
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
        public async Task<ActionResult> Edit([Bind(Include = "id,id_eval,no_tronc,id_tronc_parnt,dhp,diam_moy,haut_moy,morphlg,racdmt,qual,cavt,fent_fissre,blesr,contaminatn,sympt_visuel,possede_cime,possede_bm,est_branch_maitr,long_moy,catgr_branch_maitr,nb_branch_maitr,comm,util,dt_cretn,dt_modf")] tronc tronc)
        {
            if (ModelState.IsValid)
            {
                tronc.util = User.Identity.GetUserName();
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
