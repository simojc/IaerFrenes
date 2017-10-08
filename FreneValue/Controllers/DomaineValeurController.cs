using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreneValue.Models;
//using FreneValue.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;

namespace FreneValue.Controllers
{
    [Authorize]
    public class DomaineValeurController : Controller
    {
        // private ApplicationDbContext _db = new ApplicationDbContext();

        private arbredb _db = new arbredb();

     

        public ActionResult Index(string code, int page = 1, int pageSize = 10 )
        {
            List<COD_DOM> domains = null;

            if (code != null)
            {
                domains = _db.domaines
                                           .Where(r => r.CODE == code)
                                           .OrderBy(r => r.CODE)
                                           .ToList();
            }
            else
            {
                domains = _db.domaines
                                          .OrderBy(r => r.CODE)
                                          .ToList();
            }
            PagedList<COD_DOM> model = new PagedList<COD_DOM>(domains, page, pageSize);

            return View(model);
            }

        //public ActionResult RechercherRapid(string term)
        //{
        //    var valeurs = _db.domaines
        //              .OrderByDescending(r => r.CODE)
        //              .Where(r => r.CODE.Contains(term))
        //              .Take(10)
        //              .Select(r => new { label = r.CODE })
        //              .Distinct();

        //    return Json(valeurs, JsonRequestBehavior.AllowGet);
        //}

        //public PartialViewResult Rechercher(string q)
        //{
        //    if (q != null)
        //    {
        //        var dom = _db.domaines
        //                  .OrderByDescending(r => r.CODE)
        //                  .Where(r => r.CODE.Contains(q))
        //                  .Take(10);

        //        return PartialView("_Valeurs", dom);
        //    }
        //    else
        //    {
        //        var dom = _db.domaines
        //             .OrderByDescending(r => r.CODE)
        //             .Take(10);

        //        return PartialView("_Valeurs", dom);
        //    }
        //}


        // private  model = new arbredb();

        // GET: Domval/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COD_DOM domaine = _db.domaines.Find(id); // _db.domaines.Find(id);
            if (domaine == null)
            {
                return HttpNotFound();
            }
            return View(domaine);
        }

        // GET: Domval/Create
        public ActionResult Create()
        {
            return PartialView("_Create", new COD_DOM());
            //return View();
        }

        // POST: Domval/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "code,descrip,util,actif,dt_cretn,dt_modf")] COD_DOM domaine)
        {
            if (ModelState.IsValid)
            {
                domaine.UTIL = User.Identity.GetUserName();
                _db.domaines.Add(domaine);
                _db.SaveChanges();
                return Json(new { success = true });
                // return RedirectToAction("Index");
            }

            //return View(domaine);
            return PartialView("_Create", domaine);
        }

        // GET: Domval/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COD_DOM domaine = _db.domaines.Find(id);
            if (domaine == null)
            {
                return HttpNotFound();
            }
            return View(domaine);
        }

        // POST: Domval/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "code,DESCRIP,util,ACTIF,dt_cretn,dt_modf")] COD_DOM domaine)
        {
            if (ModelState.IsValid)
            {
                domaine.UTIL = User.Identity.GetUserName();
                _db.Entry(domaine).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(domaine);
        }

        // GET: Domval/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COD_DOM domaine = _db.domaines.Find(id);
            if (domaine == null)
            {
                return HttpNotFound();
            }
            return View(domaine);
        }

        // POST: Domval/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COD_DOM domaine = _db.domaines.Find(id);
            _db.domaines.Remove(domaine);
            _db.SaveChanges();
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


        // GET: Domval
        public ActionResult IndexVal()
        {
            return View(_db.valeurs.ToList());

            // var model = _db.domaines;  //_db.;
            // return View(model);

        }

        //CreateVal

        // GET: Domval/Details/5
        public ActionResult DetailsVal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAL_DOM valeur = _db.valeurs.Find(id); // _db.domaines.Find(id);
            if (valeur == null)
            {
                return HttpNotFound();
            }
            return View(valeur);
        }

        // GET: Domval/Create
        public ActionResult CreateVal()
        {
            return View(new VAL_DOM());
            //return View();
        }

        // POST: Domval/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVal([Bind(Include = "id,code_dom,code_val,val,DESCRIP,actif,util,dt_cretn,dt_modf")] VAL_DOM valeur)
        {
            if (ModelState.IsValid)
            {
                _db.valeurs.Add(valeur);
                _db.SaveChanges();
                return RedirectToAction("IndexVal");
            }

            return View(valeur);
        }


        // GET: Domval/Edit/5
        public ActionResult EditVal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAL_DOM valeur = _db.valeurs.Find(id);
            if (valeur == null)
            {
                return HttpNotFound();
            }
            return View(valeur);
        }

        // POST: Domval/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVal([Bind(Include = "id,code_dom,code_val,val,DESCRIP,actif,util,dt_cretn,dt_modf")] VAL_DOM valeur)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(valeur).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valeur);
        }

        // GET: Domval/Delete/5
        public ActionResult DeleteVal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAL_DOM valeur = _db.valeurs.Find(id);
            if (valeur == null)
            {
                return HttpNotFound();
            }
            return View(valeur);
        }

        public PartialViewResult ValeursDomaine(string codedom)
        {
            if (codedom != null)
            {
                var ValeursDom = _db.valeurs
                           .OrderByDescending(r => r.COD_VAL)
                           .Where(r => r.COD_DOM == codedom);

                return PartialView("_Valeurs", ValeursDom);
            }
            else
            {
                var ValeursDom = _db.valeurs
                       .OrderByDescending(r => r.COD_VAL);

                return PartialView("_Valeurs", ValeursDom);
            }
        }
    }
}
