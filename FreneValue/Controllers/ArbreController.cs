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
using System.Web;

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
                     Text = s.nom + "-" + s.num_civc + "- " + s.voie + "- " + s.ville
        }).ToList();
            ViewBag.localisation = new SelectList(w_localisation, "Value", "Text");
        }

        //[OutputCache(Duration = 60, VaryByParam = "model")]
        public ActionResult Index(ArbreSearchModel model)
        {            
            ViewBag.ESSENCE = Utilitaires.LireCodeValeurCache("ESSENCE");

            ViewBag.adresse = db.localisations;

            ViewBag.proprio = db.prof_utils; //.Where(x => x.typ_util == "PROPRIETAIRE");
           
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

            if (model.id_profil != null)
            {
                predicate = predicate.And(x => x.id_profil.ToString().ToLower().Contains(model.id_profil.ToString().ToLower()));
            }
            //  predicate = predicate.And(x => x.id = s.id_arbre);
            model.TotalRecords = (from x in db.arbres.AsQueryable().Where(predicate) select x.id).Count();
            model.arbres = (from x in db.arbres.AsQueryable().Include(s => s.Evals).Where(predicate)
                            orderby   (x.id)              /// (model.Sort  + " " + model.SortDir)
                                select x)
                                .Skip((model.Page - 1) * model.PageSize)
                                .Take(model.PageSize)
                                .ToList();


           // List<eval_abr> eval_abrs;
          
           // foreach (var s_abr in model.arbres)
           // {
           //     eval_abrs = db.evaluations.Where(c => c.id_arbre == s_abr.id).ToList(); 
           // }
           //// int count = eval_abrs.Count(); //Here you will  get count

           // ViewBag.Counts = eval_abrs.Count();

            // var r = model.arbres.ev
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


        [AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Upload(PhotoForSingleItem photo)
        public ActionResult Upload([Bind(Include = "id,id_arbre,nom,alt,image,typ_cont")] PhotoViewImage photoViewImage)             
        {
        //PhotoForSingleItem is just a class that has properties
        // Name and Alternate text.  I use strongly typed Views and Actions
        //  because I'm not a fan of using string to get the posted data from the
        //  FormCollection.  That just seems ugly and unreliable to me.

        //PhotoViewImage is just a Entityframework class that has
        // String Name, String AlternateText, Byte[] ActualImage,
        //  and String ContentType
        //  PhotoViewImage newImage = new PhotoViewImage();
            HttpPostedFileBase file = Request.Files["OriginalLocation"];
            // newImage.nom = photo.nom;
            // newImage.alt = photo.alt;

            //Here's where the ContentType column comes in handy.  By saving
            //  this to the database, it makes it infinitely easier to get it back
            //  later when trying to show the image.
            photoViewImage.typ_cont = file.ContentType;

            Int32 length = file.ContentLength;
            //This may seem odd, but the fun part is that if
            //  I didn't have a temp image to read into, I would
            //  get memory issues for some reason.  Something to do
            //  with reading straight into the object's ActualImage property.
            byte[] tempImage = new byte[length];
            file.InputStream.Read(tempImage, 0, length);
            photoViewImage.image = tempImage;

            if (ModelState.IsValid)
            {
                db.PhotoViewImage.Add(photoViewImage);
                 db.SaveChanges();
               // return RedirectToAction("Index");
            }

          //  return View(photoViewImage);

           // db.SaveChanges();

           // newImage.Save();
        
            //This part is completely optional.  You could redirect on success
            // or handle errors ect.  Just wanted to keep this simple for the example.
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowPhoto(Int32 id)
        {
            //This is my method for getting the image information
            // including the image byte array from the image column in
            // a database.
            //PhotoViewImage image = PhotoViewImage.GetById(id);
            PhotoViewImage image =  db.PhotoViewImage.Find(id);
            //As you can see the use is stupid simple.  Just get the image bytes and the
            //  saved content type.  See this is where the contentType comes in real handy.
            ImageResult result = new ImageResult(image.image, image.typ_cont);

            return result;
        }


        /// important
        /// // Utiliser cette balise pour appeler l'action et afficher la photo:
        /// //  /* <img src="/Photo/ShowPhoto/1" alt="" />*/  ou encore
        /// // <img width="150" height="150"  src="@Url.Action("GetImage", "Admin", new {Model.Id})" />
        ///// ou encore
        /// // @Html.ActionImageLink("Controller", "action/url", null, null, Url.Content("image/location"), null)

    }
}
