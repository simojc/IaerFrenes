using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FreneValue.Models;
using System.Linq.Dynamic;
using System.Web;
using Microsoft.AspNet.Identity;
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

            bool existeImgEss = db.ess_image.Count(x => x.ess_id == id) > 0;
            if (existeImgEss)
            {
                ess_image essImg = db.ess_image.Where(x => x.ess_id == essence.id).Single();
                ViewBag.id_dessn_feuil = essImg.id_dessn_feuil;
                ViewBag.id_dessn_brh = essImg.id_dessn_brh;
                ViewBag.id_dessn_abr = essImg.id_dessn_abr;
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
        public async Task<ActionResult> Create([Bind(Include = "id,nom_lat,nom_fr,nom_en,famille,origin,descrip,haut_max,diam_cime,coulr_autom,dens_bois,maladie,insecte_ravgeur,champgn_ravgeur,ph_sol,util,dt_cretn,dt_modf")] essence essence)
        {
            if (ModelState.IsValid)
            {
                essence.util = User.Identity.GetUserName();
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

            var nb_imgEss = (from x in db.ess_image.AsQueryable().Where(x => x.ess_id == essence.id) select x.id).Count();
            ViewBag.nb_imgEss = nb_imgEss;

            if (nb_imgEss > 0)
            {
                ess_image essImg = db.ess_image.Where(x => x.ess_id == essence.id).Single();

                ViewBag.id_dessn_feuil = essImg.id_dessn_feuil;
                ViewBag.id_dessn_brh = essImg.id_dessn_brh;
                ViewBag.id_dessn_abr = essImg.id_dessn_abr;
            }

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
        public async Task<ActionResult> Edit([Bind(Include = "id,nom_lat,nom_fr,nom_en,famille,origin,descrip,haut_max,diam_cime,coulr_autom,dens_bois,maladie,insecte_ravgeur,champgn_ravgeur,ph_sol,util,dt_cretn,dt_modf")]essence essence)
        {

            bool Change_dessn_feuil = false;
            bool Change_dessn_brh = false;
            bool Change_dessn_abr = false;
            ess_image essImg = new ess_image();
            Image newImage1 = new Image();
            Image newImage2 = new Image();
            Image newImage3 = new Image();
            HttpPostedFileBase file1 = Request.Files["dessn_feuil"];
            HttpPostedFileBase file2 = Request.Files["dessn_abr"];
            HttpPostedFileBase file3 = Request.Files["id_dessn_brh"];
            newImage1.nom = essence.id.ToString();
            newImage1.alt = "Photo élément essence: " + essence.nom_fr;
            newImage2.nom = essence.id.ToString();
            newImage2.alt = "Photo élément essence: " + essence.nom_fr;
            newImage3.nom = essence.id.ToString();
            newImage3.alt = "Photo élément essence: " + essence.nom_fr;

            essImg.ess_id = essence.id;

            if (file1 != null && file1.ContentLength > 0)
            {
                newImage1.typ_cont = file1.ContentType;
                int length = file1.ContentLength;
                byte[] tempImage = new byte[length];
                file1.InputStream.Read(tempImage, 0, length);
                newImage1.image = tempImage;
                newImage1.util = User.Identity.GetUserName();
                db.images.Add(newImage1);
                await db.SaveChangesAsync();
                // essence.id_dessn_feuil = newImage1.id;                           
                essImg.id_dessn_feuil = newImage1.id;
                Change_dessn_feuil = true;
            }

            if (file2 != null && file2.ContentLength > 0)
            {
                newImage2.typ_cont = file2.ContentType;
                int length = file2.ContentLength;
                byte[] tempImage = new byte[length];
                file2.InputStream.Read(tempImage, 0, length);
                newImage2.image = tempImage;
                newImage2.util = User.Identity.GetUserName();
                db.images.Add(newImage2);
                await db.SaveChangesAsync();
                // essence.id_dessn_abr = newImage2.id;
                essImg.id_dessn_abr = newImage2.id;
                Change_dessn_abr = true;
            }

            if (file3 != null && file3.ContentLength > 0)
            {
                newImage3.typ_cont = file3.ContentType;
                int length = file3.ContentLength;
                byte[] tempImage = new byte[length];
                file3.InputStream.Read(tempImage, 0, length);
                newImage3.image = tempImage;
                newImage3.util = User.Identity.GetUserName();
                db.images.Add(newImage3);
                await db.SaveChangesAsync();
                // essence.id_img_feuil = newImage3.id;
                essImg.id_dessn_brh = newImage3.id;
                Change_dessn_brh = true;
            }

            if (Change_dessn_feuil || Change_dessn_abr || Change_dessn_brh)
            {
                bool existeDeja = db.ess_image.Count(x => x.ess_id == essence.id) > 0;
                if (existeDeja)
                {
                    // Insérer ici les traitements de MAJ
                }
                else
                {
                    essImg.util = User.Identity.GetUserName();
                    essImg.ess_id = essence.id;
                    db.ess_image.Add(essImg);
                    await db.SaveChangesAsync();
                }
            }

            if (ModelState.IsValid)
            {
                essence.util = User.Identity.GetUserName();
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
