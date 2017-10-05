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


        List<string> LireCodeValeur(string w_coddom)
        {
            var DDList = db.valeurs
                           .Where(r => r.COD_DOM == w_coddom)
                           .OrderBy(r => r.VAL)
                           .Select(r => r.VAL).Distinct().ToList();
            return (DDList);
        }

        void ChargerToutesLesDDL()
        {
            ViewBag.ESSENCE = LireCodeValeur("ESSENCE");
            ViewBag.CLASSE_HAUTEUR = LireCodeValeur("CLASSE_HAUTEUR");

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
        }


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


        // GET: EvalAbr/TestTab
        public ActionResult TestTab()
        {
            return View();
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

        // GET: EvalAbr/Profil/5
        public ActionResult Profil(int? id)
        {            
            ViewBag.adresse = db.localisations.Where(x => x.num_civc != null);

            ViewBag.proprio = db.prof_utils.Where(x => x.typ_util == "PROPRIETAIRE");

             ChargerToutesLesDDL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eval_abr eval_abr =  db.evaluations.Find(id);
            if (eval_abr == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Profil", eval_abr);
        }

        // POST: EvalAbr/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profil([Bind(Include = "id,id_arbre,id_evalteur,dt_eval,dhp_tot,clas_haut,intfrce,racdmt,acesbt_manu,acesbt_machn,acesbt_cam,contrnt_transp,obstcl_sol,constrct,infrstr_urbn,typ_abatg,nb_tronc,branch_maitr,action,concl,util,dt_cretn,dt_modf")] eval_abr eval_abr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eval_abr).State = EntityState.Modified;
                 db.SaveChanges();
                  return RedirectToAction("Eval");
            }
            return PartialView("_Profil", eval_abr);
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


        public ActionResult Eval(int? id)
        {
            // return View(await db.evaluations.ToListAsync());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eval_abr eval_abr = db.evaluations.Find(id);
            if (eval_abr == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.id_arb = eval_abr.id_arbre;
            ViewBag.id_eval = eval_abr.id; 
            ViewBag.id_souche =  db.souches                          
                          .Where(r => r.id_eval == id)
                          .Select(r => r.id);
            ViewBag.List_id_tronc = db.troncs
                          .Where(r => r.id_eval == id).ToList();
            ViewBag.nb_tronc = db.troncs
                       .Where(r => r.id_eval == id).Count();

            return View();
        }

        //public ActionResult _tronc(int?id_abr)
        //{      
        //    //  var model = await _db.valeurs.ToListAsync();
        //    //ViewBag.codedom = _db.domaines.Select(r => r.CODE).Distinct();

        //    var model = _db.troncs
        //               .OrderByDescending(r => r.no_tronc)
        //               .Where(r => r.id_arbre == id_abr || (id_abr == null));

        //  //  return PartialView("_Valeurs", valeurs);
        //    return PartialView(model);
        //}

        // GET: troncs/Tronc/5
        public ActionResult Tronc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tronc tronc = db.troncs.Find(id);
            if (tronc == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Tronc", tronc);
        }

        // POST: troncs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tronc([Bind(Include = "id,id_eval,no_tronc,id_tronc_parnt,dhp,diam_moy,haut_moy,morphlg,racdmt,qual,cavt,fent_fissre,blesr,contaminatn,sympt_visuel,possede_cime,est_branch_maitr,long_moy,catgr_branch_maitr,nb_branch_maitr,comm,util,dt_cretn,dt_modf")] tronc tronc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tronc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Eval");
            }
            return PartialView("_Tronc", tronc);
        }


        // GET: souches/Edit/5
        public ActionResult Souche(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var souche = db.souches
            //               .Where(r => r.id_eval == id);
            souche souche = db.souches.Find(id);

            if (souche == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Souche", souche);
        }

        // POST: souches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Souche([Bind(Include = "id,id_eval,dhs,acces_nutrmt,aeration_sol,surf_deplmt_racin,racine_hs,blesre_racine,cavite_hrs_sol,exig_essouchmt,typ_essouchmt,profdeur_essouchmt,ray_rognage,defaut,infrstr,specificite,haut_souche,exig_abat,espace_subs,fosse_plant,comm,util,dt_cretn,dt_modf")] souche souche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(souche).State = EntityState.Modified;
                db.SaveChanges();
                 return RedirectToAction("Eval");
            }
            return PartialView("_Souche", souche);
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
