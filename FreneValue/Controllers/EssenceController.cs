
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
    public class EssenceController : Controller
    {
        private arbredb db = new arbredb();

        // GET: Essence
        public async Task<ActionResult> Index()
        {
            return View(await db.essence.ToListAsync());
        }

        // GET: Essence/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            essence essence = await db.essence.FindAsync(id);
            if (essence == null)
            {
                return HttpNotFound();
            }
            return View(essence);
        }

        // GET: Essence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Essence/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nom_lat,nom_fr,nom_en,famille,origin,descrip,typ_abr,croisnce,haut_max,diam_cime,typ_lumiere,typ_sol,coulr_autom,valo_mat_lignse,dens_bois,maladie,insecte_ravgeur,champgn_ravgeur,ph_sol,enracinemt,util,dt_cretn,dt_modf")] essence essence)
        {
            if (ModelState.IsValid)
            {
                db.essence.Add(essence);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(essence);
        }

        // GET: Essence/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            essence essence = await db.essence.FindAsync(id);
            if (essence == null)
            {
                return HttpNotFound();
            }
            return View(essence);
        }

        // POST: Essence/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nom_lat,nom_fr,nom_en,famille,origin,descrip,typ_abr,croisnce,haut_max,diam_cime,typ_lumiere,typ_sol,coulr_autom,valo_mat_lignse,dens_bois,maladie,insecte_ravgeur,champgn_ravgeur,ph_sol,enracinemt,util,dt_cretn,dt_modf")] essence essence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(essence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(essence);
        }

        // GET: Essence/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            essence essence = await db.essence.FindAsync(id);
            if (essence == null)
            {
                return HttpNotFound();
            }
            return View(essence);
        }

        // POST: Essence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            essence essence = await db.essence.FindAsync(id);
            db.essence.Remove(essence);
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
