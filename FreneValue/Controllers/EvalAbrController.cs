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
using FreneValue.Infrastructure;
using PagedList;

namespace FreneValue.Controllers
{
    public class EvalAbrController : Controller
    {
        private arbredb db = new arbredb();        

        void ChargerToutesLesDDL()
        {
            ViewBag.ESSENCE = Utilitaires.LireCodeValeurCache("ESSENCE");
            ViewBag.CLASSE_HAUTEUR = Utilitaires.LireCodeValeurCache("CLASSE_HAUTEUR");
            ViewBag.RACCORDEMENT = Utilitaires.LireCodeValeurCache("RACCORDEMENT");
            ViewBag.INTERFERENCES = Utilitaires.LireCodeValeurCache("INTERFERENCES");
            ViewBag.ACCES_MANUTENTION = Utilitaires.LireCodeValeurCache("ACCES_MANUTENTION");
            ViewBag.ACCES_MACHINERIE = Utilitaires.LireCodeValeurCache("ACCES_MACHINERIE");
            ViewBag.ACCES_CAMION = Utilitaires.LireCodeValeurCache("ACCES_CAMION");
            ViewBag.CONSTRUCTION = Utilitaires.LireCodeValeurCache("CONSTRUCTION");
            ViewBag.INFRS_URBAINE = Utilitaires.LireCodeValeurCache("INFRS_URBAINE");
            ViewBag.CONTRAINTE_TRANSP = Utilitaires.LireCodeValeurCache("CONTRAINTE_TRANSP");

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
                     Text = s.emplcmt + " - "  + s.num_civc + " - " + s.voie + " - "+ s.code_post + " - " + s.ville
                 }).ToList();
            ViewBag.localisation = new SelectList(w_localisation, "Value", "Text");
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


        void ChargerToutesLesDDLSouche()
        {
            ViewBag.SURF_DEPLOY_RACIN = Utilitaires.LireCodeValeurCache("SURF_DEPLOY_RACIN");
            ViewBag.AERATION_SOL = Utilitaires.LireCodeValeurCache("AERATION_SOL");
            ViewBag.SPECIFICITE = Utilitaires.LireCodeValeurCache("SPECIFICITE");
            ViewBag.INFRASTR_SOUCH = Utilitaires.LireCodeValeurCache("INFRASTR_SOUCH");
            ViewBag.TYPE_ESSOUCHMT = Utilitaires.LireCodeValeurCache("TYPE_ESSOUCHMT");
            ViewBag.TRAVAUX_RECOMMANDES = Utilitaires.LireCodeValeurCache("TRAVAUX_RECOMMANDES");
            ViewBag.EXIGENCE_ESSOUCHMT = Utilitaires.LireCodeValeurCache("EXIGENCE_ESSOUCHMT");
            ViewBag.SYMPT_VISUEL = Utilitaires.LireCodeValeurCache("SYMPT_VISUEL");        

            ViewBag.RACCORDEMENT = Utilitaires.LireCodeValeurCache("RACCORDEMENT");
     
        }

        // GET: EvalAbr
        //[OutputCache(Duration = 60, VaryByParam = "id_arbre")]
        public ActionResult Index1(int? id_arbre, int page = 1, int pageSize = 10)
        {
            // return View(await db.evaluations.ToListAsync());
            ViewBag.evaluateur = db.prof_utils;

            List<eval_abr> w_eval = null;

            if (id_arbre != null)
            {
                 w_eval = db.evaluations
                           .OrderByDescending(r => r.dt_eval)
                           .Where(r => r.id_arbre == id_arbre).ToList();

               // return PartialView("_Index", w_eval);
            }
            else
            {
                 w_eval = db.evaluations
                     .OrderByDescending(r => r.dt_eval).ToList();

               // return PartialView("_Index", w_eval);
            };
            PagedList<eval_abr> model = new PagedList<eval_abr>(w_eval, page, pageSize);
            return View(model);   // PartialView("_Index", model);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.evaluateur = db.prof_utils;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumrAbreSortParm = String.IsNullOrEmpty(sortOrder) ? "NumrAbre_desc" : "";
            
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var evals = from s in db.evaluations
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                evals = evals.Where(s => s.Arbre.num_arbre.Contains(searchString));
                                       //|| s.Evaluateur.NomPrem.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "NumrAbre_desc":
                    evals = evals.OrderByDescending(s => s.id_arbre);
                    break;
                case "date_desc":
                    evals = evals.OrderByDescending(s => s.dt_eval);
                    break;
                default:  // Name ascending 
                    evals = evals.OrderBy(s => s.id_arbre).ThenBy(s => s.dt_eval);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(evals.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult RechercherRapid(string term)
        {
            var valeurs = db.arbres
                      .OrderByDescending(r => r.num_arbre)
                      .Where(r => r.num_arbre.Contains(term))
                      .Take(10)
                      .Select(r => new { label = r.num_arbre })
                      .Distinct();

            return Json(valeurs, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult Rechercher(string q)
        {
            // return View(await db.evaluations.ToListAsync());
            if (q != null)
            {
                //var w_abr = db.arbres                        
                //        .Where(r => r.num_arbre.Contains(q))
                //        .Select(r => r.id).
                //        ToList();

                var w_eval = db.evaluations
                           .OrderBy(r => r.dt_eval)
                           .Where(r => r.Arbre.num_arbre.Contains(q)).ToList(); 
                        
                return PartialView("_HistoEval", w_eval);
            }
            else
            {
                var w_eval = db.evaluations
                     .OrderBy(r => r.dt_eval).ToList();

                return PartialView("_HistoEval", w_eval);
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
        public ActionResult Create(int id_arbre)
        {            
            ViewBag.adresse = db.localisations.Where(x => x.num_civc != null);

            ViewBag.proprio = db.prof_utils.Where(x => x.typ_util == "PROPRIETAIRE");

             ChargerToutesLesDDL();

            eval_abr model = new eval_abr();
            model.id_arbre = id_arbre;
            return View(model);
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
                eval_abr.util = User.Identity.GetUserName();
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
                eval_abr.util = User.Identity.GetUserName();
                db.Entry(eval_abr).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eval_abr);
        }

        // GET: EvalAbr/Profil/5
        public ActionResult Profil(int? id)
        {
            eval_abr eval_abr = db.evaluations.Find(id);
            ViewBag.adresse = db.localisations.Where(x => x.num_civc != null);

            ViewBag.proprio = db.prof_utils.Where(x => x.typ_util == "PROPRIETAIRE");

             ChargerToutesLesDDL();

            int nb_souche = db.souches
                      .Where(r => r.id_eval == id)
                      .Select(r => r.id).Count();
            bool SoucheExiste = (nb_souche > 0);
            ViewBag.SoucheNotExiste = !SoucheExiste;

            int nb_tronc = db.troncs
                                .Where(r => r.id_eval == id)
                                .Select(r => r.id).Count();

            bool nbTroncAtteind = (nb_tronc == eval_abr.nb_tronc);
            ViewBag.nbTroncAtteind = nbTroncAtteind;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
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
                eval_abr.util = User.Identity.GetUserName();
                db.Entry(eval_abr).State = EntityState.Modified;
                 db.SaveChanges();
                  return RedirectToAction("Eval", new { id = eval_abr.id });
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

       // [OutputCache(Duration = 60, VaryByParam = "id")]
        public ActionResult Eval(int? id)
        {
            // return View(await db.evaluations.ToListAsync());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eval_abr eval_abr = db.evaluations.Find(id);
            arbre abr = db.arbres.Find(eval_abr.id_arbre);
            var profilUtil = db.prof_utils.Find(abr.id_profil);
            ViewBag.profilUtil = profilUtil.nom + " " + profilUtil.pren;
                            
            if (eval_abr == null)
            {
                return HttpNotFound();
            }
            ViewBag.arbre = abr;
            ViewBag.id_arbre = eval_abr.id_arbre;
            ViewBag.id_eval = eval_abr.id;
            ViewBag.id_souche = db.souches
                          .Where(r => r.id_eval == id)
                          .Select(r => r.id)
                          .FirstOrDefault();
            ViewBag.List_id_tronc = db.troncs
                          .Where(r => r.id_eval == id).ToList();
            ViewBag.nb_tronc = db.troncs
                       .Where(r => r.id_eval == id).Count();

            return View();
        }

       // [OutputCache(Duration = 60, VaryByParam = "id")]
        public ActionResult EvalLast(int id)
        {
            // return View(await db.evaluations.ToListAsync());
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            arbre abr = db.arbres.Find(id);            
            //DateTime? maxdateval = db.evaluations.Where(x => x.id_arbre == id).Max(x => x.dt_eval);
            //eval_abr eval_abr1 = db.evaluations.Where(x => x.id_arbre == id).First(x => x.dt_eval == maxdateval);

            eval_abr eval_abr = db.evaluations.Where(x => x.id_arbre == id)
                                .OrderByDescending(x => x.dt_eval)
                                .FirstOrDefault();

            var profilUtil = db.prof_utils.Find(abr.id_profil);
            ViewBag.profilUtil = profilUtil.nom + " " + profilUtil.pren;
       
            ViewBag.arbre = abr;
            ViewBag.id_arbre = eval_abr.id_arbre;
            ViewBag.id_eval = eval_abr.id;
            ViewBag.id_souche = db.souches
                          .Where(r => r.id_eval == id)
                          .Select(r => r.id)
                          .FirstOrDefault();
            ViewBag.List_id_tronc = db.troncs
                          .Where(r => r.id_eval == id).ToList();
            ViewBag.nb_tronc = db.troncs
                       .Where(r => r.id_eval == id).Count();

            return View();
        }

        // GET: troncs/Tronc/5
        public ActionResult Tronc(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tronc tronc = db.troncs.Find(id);
            var w_troncs = db.troncs
           .Where(s => s.id_eval == tronc.id_eval)
             .Select(s => new SelectListItem
             {
                 Value = s.id.ToString(),
                 Text =  s.no_tronc.ToString()
             }).ToList();
          //  w_troncs.Add(new SelectListItem() { Value = String.Empty, Text = String.Empty });
            ViewBag.w_tronc = new SelectList(w_troncs, "Value", "Text");

            int nb_cime = db.cimes
                      .Where(r => r.id_tronc == id)
                      .Select(r => r.id).Count();
            bool CimeExiste = (nb_cime > 0);
            ViewBag.CimeExiste = CimeExiste;
            ChargerToutesLesDDLTronc();
           
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
                tronc.util = User.Identity.GetUserName();
                db.Entry(tronc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Eval", new { id = tronc.id_eval });
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

            ChargerToutesLesDDLSouche();

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
        public ActionResult Souche([Bind(Include = "id,id_eval,dhs,acces_nutrmt,aeration_sol,surf_deplmt_racin,racine_hs,blesre_racine,cavite_au_collet,exig_essouchmt,typ_essouchmt,profdeur_essouchmt,ray_rognage,defaut,infrstr,specificite,haut_souche,exig_abat,espace_subs,fosse_plant,comm,util,dt_cretn,dt_modf")] souche souche)
        {
            if (ModelState.IsValid)
            {
                souche.util = User.Identity.GetUserName();
                db.Entry(souche).State = EntityState.Modified;
                db.SaveChanges();
                 return RedirectToAction("Eval" ,new { id = souche.id_eval });
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
