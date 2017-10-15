using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FreneValue.Models;
using FreneValue.ViewModels;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using LinqKit;
//using System.Web.Caching;
using FreneValue.Infrastructure;

namespace FreneValue.Controllers
{
    public class ArbreController : Controller
    {
        private arbredb db = new arbredb();
       
        void ChargerToutesLesDDL()
        {
            ViewBag.ESSENCE = Utilitaires.LireCodeValeurCache("ESSENCE");
            ViewBag.ORIENTATION = Utilitaires.LireCodeValeurCache("ORIENTATION");
            ViewBag.TYPE_EMPLACEMENT = Utilitaires.LireCodeValeurCache("TYPE_EMPLACEMENT");
            //ViewBag.EMPLACEMENT = Utilitaires.LireCodeValeurCache("EMPLACEMENT");
            var w_profilUtil = db.prof_utils
                  .Select(s => new SelectListItem
                  {
                      Value = s.id.ToString(),
                      Text = s.nom +" "+ s.pren 
                  }).ToList();
            ViewBag.profilUtil = new SelectList(w_profilUtil, "Value", "Text");
            var w_localisation = db.localisations
                 .Select(s => new SelectListItem
                 {
                     Value = s.id.ToString(),
                     Text = s.emplcmt +" - " + s.code_post + " - " + s.num_civc + " - " + s.nom_rue + " - " + s.ville
                 }).ToList();
            ViewBag.localisation = new SelectList(w_localisation, "Value", "Text");
        }

        [OutputCache(Duration = 60, VaryByParam = "model")]
        public ActionResult Index(ArbreSearchModel model)
        {            
            ViewBag.ESSENCE = Utilitaires.LireCodeValeurCache("ESSENCE");

            ViewBag.adresse = db.localisations.Where(x => x.num_civc != null );

            ViewBag.proprio = db.prof_utils.Where(x => x.typ_util == "PROPRIETAIRE");
           
            var predicate = PredicateBuilder.True<arbre>();
            if (model.num_arbre != null)
            {
                predicate = predicate.And(x => x.num_arbre.ToLower().Contains(model.num_arbre.ToLower() ));
            }

            if (model.ess != null)
            {
                predicate = predicate.And(x => x.ess.ToLower().Contains(model.ess.ToLower()));
            }

            if (model.id_local != null)
            {
                predicate = predicate.And(x => x.id_local.ToString().ToLower().Contains(model.id_local.ToString().ToLower()));
            }
            model.TotalRecords = (from x in db.arbres.AsQueryable().Where(predicate) select x.id).Count();
            model.arbres = (from x in db.arbres.AsQueryable().Where(predicate) 
                              orderby   (x.id)              /// (model.Sort  + " " + model.SortDir)
                                select x)
                                .Skip((model.Page - 1) * model.PageSize)
                                .Take(model.PageSize)
                                .ToList();           
            return View(model);
        }

        // GET: Arbre/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arbre arbre = await db.arbres.FindAsync(id);
            if (arbre == null)
            {
                return HttpNotFound();
            }
            return View(arbre);
        }

        // GET: Arbre/Create
        public ActionResult Create()
        {
            ChargerToutesLesDDL();
            return View();
        }

        // POST: Arbre/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,num_arbre,id_profil,id_local,typ_emplcmt,emplcmt,orientatn,ess,lattd,longtd,dt_plant,dhp_tot,nb_tronc,type_lieu,typ_abr,typ_prop,nom_topo,util,dt_cretn,dt_modf")] arbre arbre)
        {
            if (ModelState.IsValid)
            {
                arbre.util = User.Identity.GetUserName();
                db.arbres.Add(arbre);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
           }
            ChargerToutesLesDDL();
            return View(arbre);
        }

        // GET: Arbre/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ChargerToutesLesDDL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arbre arbre = await db.arbres.FindAsync(id);
            if (arbre == null)
            {
                return HttpNotFound();
            }
            return View(arbre);
        }

        // POST: Arbre/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,num_arbre,id_profil,id_local,typ_emplcmt,emplcmt,orientatn,ess,lattd,longtd,dt_plant,dhp_tot,nb_tronc,type_lieu,typ_abr,typ_prop,nom_topo,util,dt_cretn,dt_modf")] arbre arbre)
        {
            if (ModelState.IsValid)
            {
                arbre.util = User.Identity.GetUserName();
                db.Entry(arbre).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(arbre);
        }

        // GET: Arbre/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arbre arbre = await db.arbres.FindAsync(id);
            if (arbre == null)
            {
                return HttpNotFound();
            }
            return View(arbre);
        }

        // POST: Arbre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            arbre arbre = await db.arbres.FindAsync(id);
            db.arbres.Remove(arbre);
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

        public ActionResult Evaluation(int? id_arbre)
        {
            // return View(await db.evaluations.ToListAsync());

            ViewBag.id_arbre = id_arbre;

            if (id_arbre != null)
            {
                var w_eval = db.evaluations
                           .OrderByDescending(r => r.dt_eval)
                           .Where(r => r.id_arbre == id_arbre);

                return PartialView("_Evaluation", w_eval);
            }
            else
            {
                var w_eval = db.evaluations
                     .OrderByDescending(r => r.dt_eval);

                return PartialView("_Evaluation", w_eval);
            }
        }

        
        // POST: Profil/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
   

        //public ActionResult _tronc(int?id_abr)
        //{      
        //    //  var model = await _db.valeurs.ToListAsync();
        //    //ViewBag.codedom = _db.domaines.Select(r => r.code).Distinct();

        //    var model = _db.troncs
        //               .OrderByDescending(r => r.no_tronc)
        //               .Where(r => r.id_arbre == id_abr || (id_abr == null));

        //  //  return PartialView("_Valeurs", valeurs);
        //    return PartialView(model);
        //}


       




    }
}
