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
using System.Linq.Dynamic;

using Microsoft.AspNet.Identity;
using LinqKit;

namespace FreneValue.Controllers
{
    public class ArbreController : Controller
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


        // GET: Arbre
        public async Task<ActionResult> Index1()
        {
            return View(await db.arbres.ToListAsync());
        }


        public ActionResult Index(ArbreSearchModel model)
        {
            // To Bind the category drop down in search section
           // ViewBag.Categories = db.arbres;

            ViewBag.ESSENCE = LireCodeValeur("ESSENCE");

            ViewBag.adresse = db.localisations.Where(x => x.num_civc != null );

            ViewBag.proprio = db.prof_utils.Where(x => x.typ_util == "PROPRIETAIRE");

            // total records for paging
            //model.TotalRecords = db.arbres
            //    .Count(x =>
            //       ((model.num_arbre == null) || (x.num_arbre.Contains(model.num_arbre)))
            //        && (model.ess == null || x.ess == model.ess)
            //        && (model.id_local == null || x.id_local == model.id_local)
            //       );

            //var orderByResult = from s in studentList
            //                    orderby s.StudentName, s.Age
            //                    select new { s.StudentName, s.Age };

            //var totalAge = (from s in studentList
            //                select s.age).Count();



            //if (firstName != string.Empty)
            //{
            //    predicate = predicate.And(p => p.User.FirstName.ToLower().Contains(firstName.ToLower()));
            //}

            //if (lastName != string.Empty)
            //{
            //    predicate = predicate.And(p => p.User.LastName.ToLower().Contains(lastName.ToLower()));
            //}


            //a = from r in results.AsQueryable().Where(predicate) select r;
            //return a;


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
            //where ((string.Compare(model.num_arbre, null, true) == 0 || x.num_arbre.Contains(model.num_arbre))
            // && (string.Compare(model.ess, null, true) == 0 || x.ess == model.ess)
            // && (string.Compare(model.id_local.ToString(),null, true) == 0 || x.id_local == model.id_local))


            model.arbres = (from x in db.arbres.AsQueryable().Where(predicate) 
                                //where ((string.Compare(model.num_arbre, "", true) == 0 || x.num_arbre.Contains(model.num_arbre))
                                //      && (string.Compare(model.ess, "", true) == 0 || x.ess == model.ess)
                                //      && (string.Compare(model.id_local.ToString(), "", true) == 0 || x.id_local == model.id_local))
                            orderby   (x.id)              /// (model.Sort  + " " + model.SortDir)
                                select x)
                                .Skip((model.Page - 1) * model.PageSize)
                                .Take(model.PageSize)
                                .ToList();

            //Dim skipResult = From s In studentList
            //     Skip 3
            //     Select s


            //Dim takeResult = From s In studentList
            //     Take 3
            //     Select s

            // Get Products
            //model.arbres = db.arbres
            //    .Where(
            //        x =>
            //        (model.num_arbre == null || x.num_arbre.Contains(model.num_arbre))
            //        && (model.ess == null  ||  x.ess == model.ess)
            //        && (model.id_local == null || x.id_local == model.id_local)
            //       )
            //    .OrderBy(model.Sort + " " + model.SortDir)
            //    .Skip((model.Page - 1) * model.PageSize)
            //    .Take(model.PageSize)
            //    .ToList();

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
        public async Task<ActionResult> Create([Bind(Include = "id,num_arbre,id_profil,id_local,typ_emplcmt,orientatn,ess,lattd,longtd,dt_plant,type_lieu,typ_abr,typ_prop,nom_topo,util,dt_cretn,dt_modf")] arbre arbre)
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
        public async Task<ActionResult> Edit([Bind(Include = "id,num_arbre,id_profil,id_local,typ_emplcmt,orientatn,ess,lattd,longtd,dt_plant,type_lieu,typ_abr,typ_prop,nom_topo,util,dt_cretn,dt_modf")] arbre arbre)
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

        // GET: Profil/_Profil/5
        public ActionResult Profil(int? id_arb)
        {
            //ViewBag.ESSENCE = CreerDDL("ESSENCE");

            //ViewBag.CLASSE_HAUTEUR = CreerDDL("CLASSE_HAUTEUR");

            //ViewBag.CLASSE_HAUTEUR = CreerDDL("CLASSE_HAUTEUR");

            ChargerToutesLesDDL();

            //  .Where(r => r.COD_DOM == "ESSENCE")
            if (id_arb == null)
            {
                //id_arb = 41;
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arbre arb = db.arbres.Find(id_arb);
            if (arb == null)
            {
                return HttpNotFound();
            }
            // return View(tOTO);

            return PartialView(arb);
        }


        // POST: Profil/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
   

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


        // GET: Tronc/Edit/5
        public ActionResult tronc(int? id , int? id_arbre)
        {
            if (id == null || id_arbre == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tronc = from s in db.troncs
                       orderby (s.id)
                       where ((s.id_eval == id_arbre) && (s.id == id))
                       select s;            
            if (tronc == null)
            {
                return HttpNotFound();
            }
            return PartialView("_tronc", tronc);
        }

        // POST: Tronc/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> tronc([Bind(Include = "id,id_arbre,id_tronc_parnt,no_tronc,dhp,diam_moy,Haut_moy,morphlg,racdmt,qual,cavt,fent_fissre,blesr,contaminatn,sympt_visuel,possede_cime,est_branch_maitr,long_moy,catgr_branch_maitr,nb_branch_maitr,comm,util,dt_cretn,dt_modf")] tronc tronc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tronc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_tronc", tronc);
        }





    }
}
