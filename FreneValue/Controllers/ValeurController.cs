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
using Microsoft.AspNet.Identity;
using PagedList;
namespace FreneValue.Controllers
{
    [Authorize]
    public class ValeurController : Controller
    {
        private arbredb _db = new arbredb();
       // private user = new User.Identity;      
        // GET: Valeur
        public  ActionResult Index(string codedom, int page = 1, int pageSize = 10)
        {
            //  var model = await _db.valeurs.ToListAsync();
            ViewBag.codedom = _db.domaines.OrderBy(r => r.code).Select(r => r.code).Distinct();
            List<val_dom> vals = null;          
            if (codedom != null)
            { 
             vals = _db.valeurs
                       .OrderBy(r => r.code_dom).ThenBy(r => r.code_val)
                       .Where(r => r.code_dom.Contains(codedom) )
                       .ToList();
                //return View(model);
            }
            else
            {
                vals = _db.valeurs
                       .OrderBy(r => r.code_dom).ThenBy(r => r.code_val)
                        .ToList(); ;
               // return View(model);
            }
            PagedList<val_dom> model = new PagedList<val_dom>(vals, page, pageSize);
            //return PartialView("_Valeur1", model);
            return View(model); 
        }
            
        public ActionResult RechercherRapid(string term)
        {
            var valeurs = _db.valeurs
                      .OrderBy(r => r.code_val)
                      .Where(r => r.code_dom.Contains(term) )
                      .Take(10)
                      .Select(r => new { label = r.code_dom })
                      .Distinct();

            return Json(valeurs, JsonRequestBehavior.AllowGet); 
        }

        public PartialViewResult Rechercher(string q, int page = 1, int pageSize = 10)
        {if (q != null)
            {                
                var valeurs = _db.valeurs
                     .OrderBy(r => r.code_dom).ThenBy(r => r.code_val)
                      .Where(r => r.code_dom.Contains(q)  )
                      .ToList();

                PagedList<val_dom> model = new PagedList<val_dom>(valeurs, page, pageSize);
            //return View(model);

              //  return PartialView("_Valeurs", model);
                return PartialView("_Valeur1", model);
            }
        else
            {
                var valeurs = _db.valeurs
                     .OrderBy(r => r.code_dom).ThenBy(r => r.code_val)
                     .ToList(); 
                PagedList<val_dom> model = new PagedList<val_dom>(valeurs, page, pageSize);
                //return View(model);

               // return PartialView("_Valeurs", model);
                return PartialView("_Valeur1", model);
            }
        }

        // GET: Valeur/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            val_dom valeur = await _db.valeurs.FindAsync(id);
            if (valeur == null)
            {
                return HttpNotFound();
            }
            return View(valeur);
        }

        // GET: Valeur/Create
        public ActionResult Create()
        {
            ViewBag.codedom = _db.domaines.OrderBy(r => r.code).Select(r => r.code).Distinct() ;
            return View();
        }

        // POST: Valeur/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,code_dom,code_val,val,descrip,util,actif,dt_cretn,dt_modf")] val_dom valeur)
        {
            if (ModelState.IsValid)
            {
                valeur.util = User.Identity.GetUserName();
                _db.valeurs.Add(valeur);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(valeur);
        }

        // GET: Valeur/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.codedom = _db.domaines.OrderBy(r => r.code).Select(r => r.code).Distinct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            val_dom valeur = await _db.valeurs.FindAsync(id);
            if (valeur == null)
            {
                return HttpNotFound();
            }
            return View(valeur);
        }

        // POST: Valeur/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,code_dom,code_val,val,descrip,util,actif,dt_cretn,dt_modf")] val_dom valeur)
        {
            if (ModelState.IsValid)
            {
                valeur.util = User.Identity.GetUserName();
                _db.Entry(valeur).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(valeur);
        }

        // GET: Valeur/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            val_dom Domain = await _db.valeurs.FindAsync(id);
            if (Domain == null)
            {
                return HttpNotFound();
            }
            return View(Domain);
        }

        // POST: Valeur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            val_dom Domain = await _db.valeurs.FindAsync(id);
            _db.valeurs.Remove(Domain);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
