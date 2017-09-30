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
namespace FreneValue.Controllers
{
    [Authorize]
    public class ValeurController : Controller
    {
        private arbredb _db = new arbredb();

       // private user = new User.Identity;      

        // GET: Valeur
        public  ActionResult Index(string codedom)
        {
            //  var model = await _db.valeurs.ToListAsync();
            ViewBag.codedom = _db.domaines.Select(r => r.CODE).Distinct();
            if (codedom != null)
            { 
            var model = _db.valeurs
                       .OrderByDescending(r => r.COD_DOM)
                       .Where(r => r.COD_DOM == codedom );
                return View(model);
            }
            else
            {
                var model = _db.valeurs
                       .OrderByDescending(r => r.COD_DOM);
                return View(model);
            }
            
        }


        public ActionResult RechercherRapid(string term)
        {
            var valeurs = _db.valeurs
                      .OrderByDescending(r => r.COD_VAL)
                      .Where(r => r.COD_DOM.Contains(term) )
                      .Take(10)
                      .Select(r => new { label = r.COD_DOM })
                      .Distinct();

            return Json(valeurs, JsonRequestBehavior.AllowGet); 
        }

        public PartialViewResult Rechercher(string q)
        {if (q != null)
            { 
            var valeurs = _db.valeurs
                      .OrderByDescending(r => r.COD_VAL)
                      .Where(r => r.COD_DOM.Contains(q)  )
                      .Take(10);

            return PartialView("_Valeurs", valeurs);
            }
        else
            {
                var valeurs = _db.valeurs
                     .OrderByDescending(r => r.COD_VAL)
                     .Take(10);

                return PartialView("_Valeurs", valeurs);
            }
        }



        // GET: Valeur/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAL_DOM vAL_DOM = await _db.valeurs.FindAsync(id);
            if (vAL_DOM == null)
            {
                return HttpNotFound();
            }
            return View(vAL_DOM);
        }

        // GET: Valeur/Create
        public ActionResult Create()
        {
            ViewBag.codedom = _db.domaines.Select(r => r.CODE).Distinct();
            return View();
        }

        // POST: Valeur/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,COD_DOM,COD_VAL,VAL,DESCRIP,UTIL,ACTIF,DT_CRETN,DT_MODF")] VAL_DOM vAL_DOM)
        {
            if (ModelState.IsValid)
            {
                vAL_DOM.UTIL = User.Identity.GetUserName();
                _db.valeurs.Add(vAL_DOM);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vAL_DOM);
        }

        // GET: Valeur/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.codedom = _db.domaines.Select(r => r.CODE).Distinct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAL_DOM vAL_DOM = await _db.valeurs.FindAsync(id);
            if (vAL_DOM == null)
            {
                return HttpNotFound();
            }
            return View(vAL_DOM);
        }

        // POST: Valeur/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,COD_DOM,COD_VAL,VAL,DESCRIP,UTIL,ACTIF,DT_CRETN,DT_MODF")] VAL_DOM vAL_DOM)
        {
            if (ModelState.IsValid)
            {
                vAL_DOM.UTIL = User.Identity.GetUserName();
                _db.Entry(vAL_DOM).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vAL_DOM);
        }

        // GET: Valeur/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAL_DOM vAL_DOM = await _db.valeurs.FindAsync(id);
            if (vAL_DOM == null)
            {
                return HttpNotFound();
            }
            return View(vAL_DOM);
        }

        // POST: Valeur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VAL_DOM vAL_DOM = await _db.valeurs.FindAsync(id);
            _db.valeurs.Remove(vAL_DOM);
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
