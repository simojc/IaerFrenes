using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FreneValue.Models;
using FreneValue.ViewModels;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;

using System.Web;

namespace FreneValue.Controllers
{
    public class EssenceController : Controller
    {
        private arbredb db = new arbredb();

        // GET: Essence
        public async Task<ActionResult> Index()
        {
            return View(await db.essence.ToListAsync());
        }

        // GET: Essence/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            essence essence = await db.essence.FindAsync(id);
            if (essence == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Detail", essence);
           // return View(essence);           
        }

        // GET: Essence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Essence/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nom_lat,nom_fr,nom_en,famille,origin,descrip,typ_abr,croisnce,haut_max,diam_cime,typ_lumiere,typ_sol,coulr_autom,id_dessn_feuil, id_img_feuil, id_dessn_abr, id_img_abr,valo_mat_lignse,dens_bois,maladie,insecte_ravgeur,champgn_ravgeur,ph_sol,enracinemt,util,dt_cretn,dt_modf")] essence essence)
        {           
            if (ModelState.IsValid)
            {
                db.essence.Add(essence);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(essence);
        }

        // GET: Essence/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            essence essence = await db.essence.FindAsync(id);
            if (essence == null)
            {
                return HttpNotFound();
            }
            return View(essence);
        }

        // POST: Essence/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nom_lat,nom_fr,nom_en,famille,origin,descrip,typ_abr,croisnce,haut_max,diam_cime,typ_lumiere,typ_sol,coulr_autom,id_dessn_feuil, id_img_feuil, id_dessn_abr, id_img_abr,valo_mat_lignse,dens_bois,maladie,insecte_ravgeur,champgn_ravgeur,ph_sol,enracinemt,util,dt_cretn,dt_modf")] essence essence)
        {
            Image newImage = new Image();
            HttpPostedFileBase file1 = Request.Files["dessn_feuil"];
            HttpPostedFileBase file2 = Request.Files["dessn_abr"];
            HttpPostedFileBase file3 = Request.Files["img_feuil"];
            newImage.nom = essence.id.ToString();
            newImage.alt = "Photo élément essence: " + essence.nom_fr;

            if (file1 != null && file1.ContentLength > 0)
            {
                newImage.typ_cont = file1.ContentType;
                int length = file1.ContentLength;
                byte[] tempImage = new byte[length];
                file1.InputStream.Read(tempImage, 0, length);
                newImage.image = tempImage;
                db.images.Add(newImage);
                await db.SaveChangesAsync();
                essence.id_dessn_feuil = newImage.id;
            }

            if (file2 != null && file2.ContentLength > 0)
            {
                newImage.typ_cont = file2.ContentType;
                int length = file2.ContentLength;
                byte[] tempImage = new byte[length];
                file2.InputStream.Read(tempImage, 0, length);
                newImage.image = tempImage;
                db.images.Add(newImage);
                await db.SaveChangesAsync();
                essence.id_dessn_abr = newImage.id;
            }

            if (file3 != null && file3.ContentLength > 0)
            {
                newImage.typ_cont = file3.ContentType;
                int length = file3.ContentLength;
                byte[] tempImage = new byte[length];
                file3.InputStream.Read(tempImage, 0, length);
                newImage.image = tempImage;
                db.images.Add(newImage);
                await db.SaveChangesAsync();
                essence.id_img_feuil = newImage.id;
            }

            if (ModelState.IsValid)
            {
                db.Entry(essence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(essence);
        }

        // GET: Essence/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            essence essence = await db.essence.FindAsync(id);
            if (essence == null)
            {
                return HttpNotFound();
            }
            return View(essence);
        }

        // POST: Essence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            essence essence = await db.essence.FindAsync(id);
            db.essence.Remove(essence);
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowPhoto(Int32 id)
        {
            //This is my method for getting the image information
            // including the image byte array from the image column in
            // a database.
            Image image = db.images.Find(id);
            //As you can see the use is stupid simple.  Just get the image bytes and the
            //  saved content type.  See this is where the contentType comes in real handy.
            ImageResult result = new ImageResult(image.image, image.typ_cont);

            return result;
        }
    }
}
