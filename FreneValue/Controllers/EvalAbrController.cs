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
    public class EvalAbrController : Controller
    {
        private arbredb db = new arbredb();

        // GET: EvalAbr
        public ActionResult Index(int? id_arbre)
        {
           // return View(await db.evaluations.ToListAsync());

            if (id_arbre != null)
            {
                var w_eval = db.evaluations
                           .OrderByDescending(r => r.dt_eval)
                           .Where(r => r.id_arbre == id_arbre);

                return PartialView("_Index", w_eval);
            }
            else
            {
                var w_eval = db.evaluations
                     .OrderByDescending(r => r.dt_eval);

                return PartialView("_Index", w_eval);
            }

        }

        // GET: EvalAbr/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eval_abr eval_abr = await db.evaluations.FindAsync(id);
            if (eval_abr == null)
            {
                return HttpNotFound();
            }
            return View(eval_abr);
        }

        // GET: EvalAbr/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvalAbr/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_arbre,id_evalteur,dt_eval,dhp_tot,clas_haut,intfrce,racdmt,acesbt_manu,acesbt_machn,acesbt_cam,contrnt_transp,obstcl_sol,constrct,infrstr_urbn,typ_abatg,nb_tronc,branch_maitr,action,concl,util,dt_cretn,dt_modf")] eval_abr eval_abr)
        {
            if (ModelState.IsValid)
            {
                db.evaluations.Add(eval_abr);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eval_abr);
        }

        // GET: EvalAbr/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eval_abr eval_abr = await db.evaluations.FindAsync(id);
            if (eval_abr == null)
            {
                return HttpNotFound();
            }
            return View(eval_abr);
        }

        // POST: EvalAbr/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,id_arbre,id_evalteur,dt_eval,dhp_tot,clas_haut,intfrce,racdmt,acesbt_manu,acesbt_machn,acesbt_cam,contrnt_transp,obstcl_sol,constrct,infrstr_urbn,typ_abatg,nb_tronc,branch_maitr,action,concl,util,dt_cretn,dt_modf")] eval_abr eval_abr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eval_abr).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eval_abr);
        }

        // GET: EvalAbr/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eval_abr eval_abr = await db.evaluations.FindAsync(id);
            if (eval_abr == null)
            {
                return HttpNotFound();
            }
            return View(eval_abr);
        }

        // POST: EvalAbr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            eval_abr eval_abr = await db.evaluations.FindAsync(id);
            db.evaluations.Remove(eval_abr);
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
