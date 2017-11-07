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
using PagedList;
using FreneValue.Models;

namespace FreneValue.Controllers
{
    public class LocalisationController : Controller
    {
        private arbredb db = new arbredb();

        // GET: Localisation
        public async Task<ActionResult> Index_1()
        {
            return View(await db.localisations.ToListAsync());
        }


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.typ_locSortParm = String.IsNullOrEmpty(sortOrder) ? "typ_loc_desc" : "";
            ViewBag.villeSortParm = String.IsNullOrEmpty(sortOrder) ? "ville_desc" : "";
            ViewBag.code_postSortParm = String.IsNullOrEmpty(sortOrder) ? "code_post_desc" : "";
            

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var localisation = from s in db.localisations
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                localisation = localisation.Where(s => s.typ_loc.Contains(searchString)
                                       || s.ville.Contains(searchString)
                                       || s.code_post.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "typ_loc_desc":
                    localisation = localisation.OrderByDescending(s => s.typ_loc);
                    break;
                case "ville_desc":
                    localisation = localisation.OrderByDescending(s => s.ville);
                    break;
                default:  // Name ascending 
                    localisation = localisation.OrderBy(s => s.typ_loc).ThenBy(s => s.ville);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(localisation.ToPagedList(pageNumber, pageSize));
        }


        // GET: Localisation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loclsn loclsn = await db.localisations.FindAsync(id);
            if (loclsn == null)
            {
                return HttpNotFound();
            }
            return View(loclsn);
        }

       
        // GET: Localisation/Create
        public ActionResult Create(string type)
        {
            List<SelectListItem> myListtyploc = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="Terrain en zone urbanisée"},
                 new SelectListItem{ Value="2",Text="Voie de transport"},
                 new SelectListItem{ Value="3",Text="Cours d'eau"}
            };
            myListtyploc = data.ToList();

            ViewBag.typloc = new SelectList(myListtyploc, "Value", "Text");

            loclsn model = new loclsn();
            
            model.prov = "Québec";
            model.pays = "Canada";

            if (type == "1")
                model.typ_loc = "Terrain en zone urbanisée";
            else
            if (type == "2")
                model.typ_loc = "Voie de transport";
            else
            if (type == "3")
                model.typ_loc = "Cours d'eau";
            else
                model.typ_loc = null;

            if (type == null || type == "")
            {
                return PartialView("_Create");
            }
            else
            if (type == "1")  
                return PartialView("_Create1", model);
            else
            if (type == "2" || type == "3")
                return PartialView("_Create2", model);
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Localisation/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,typ_loc,num_civc,voie,lot,mtrle,nom,tronc_rue,sectn_cours_eau,emplcmt,code_post,ville,prov,orient,geomtr,longueur,pays,suprfc,lattd_a,longtd_a,lattd_b,longtd_b,util,dt_cretn,dt_modf")] loclsn loclsn)
        {
            if (ModelState.IsValid)
            {
                loclsn.util = User.Identity.GetUserName();
                db.localisations.Add(loclsn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loclsn);
        }

        // GET: Localisation/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loclsn loclsn =  db.localisations.Find(id);
            if (loclsn == null)
            {
                return HttpNotFound();
            }
            if (loclsn.typ_loc.Contains("Terrain"))
                return PartialView("_Edit1", loclsn);
            else
           if (loclsn.typ_loc.Contains("transport") || loclsn.typ_loc.Contains("Cours"))
                return PartialView("_Edit2", loclsn);
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //return View(loclsn);
        }

        // POST: Localisation/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,typ_loc,num_civc,voie,lot,mtrle,nom,tronc_rue,sectn_cours_eau,emplcmt,code_post,ville,prov,orient,geomtr,longueur,pays,suprfc,lattd_a,longtd_a,lattd_b,longtd_b,util,dt_cretn,dt_modf")] loclsn loclsn)
        {
            if (ModelState.IsValid)
            {
                loclsn.util = User.Identity.GetUserName();
                db.Entry(loclsn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loclsn);
        }

        public ActionResult RechercherRapid(string term)
        {
            term = term.ToUpper();
            var valeurs = db.codepostal
                      .OrderBy(r => r.cod_post)
                      .Where(r => r.cod_post.Contains(term))
                      .Take(10)
                      .Select(r => new { label = r.cod_post })
                      .Distinct();

            return Json(valeurs, JsonRequestBehavior.AllowGet);
        }

        // GET: Localisation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loclsn loclsn = await db.localisations.FindAsync(id);
            if (loclsn == null)
            {
                return HttpNotFound();
            }
            return View(loclsn);
        }

        // POST: Localisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            loclsn loclsn = await db.localisations.FindAsync(id);
            db.localisations.Remove(loclsn);
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
