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
    public class PhotoViewImagesController : Controller
    {
        private arbredb db = new arbredb();

        // GET: PhotoViewImages
        public async Task<ActionResult> Index()
        {
            return View(await db.PhotoViewImage.ToListAsync());
        }

        // GET: PhotoViewImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoViewImage photoViewImage = await db.PhotoViewImage.FindAsync(id);
            if (photoViewImage == null)
            {
                return HttpNotFound();
            }
            return View(photoViewImage);
        }

        // GET: PhotoViewImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhotoViewImages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,id_arbre,nom,alt,image,typ_cont")] PhotoViewImage photoViewImage)
        {
            if (ModelState.IsValid)
            {
                db.PhotoViewImage.Add(photoViewImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(photoViewImage);
        }

        // GET: PhotoViewImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoViewImage photoViewImage = await db.PhotoViewImage.FindAsync(id);
            if (photoViewImage == null)
            {
                return HttpNotFound();
            }
            return View(photoViewImage);
        }

        // POST: PhotoViewImages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,id_arbre,nom,alt,image,typ_cont")] PhotoViewImage photoViewImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photoViewImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(photoViewImage);
        }

        // GET: PhotoViewImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoViewImage photoViewImage = await db.PhotoViewImage.FindAsync(id);
            if (photoViewImage == null)
            {
                return HttpNotFound();
            }
            return View(photoViewImage);
        }

        // POST: PhotoViewImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhotoViewImage photoViewImage = await db.PhotoViewImage.FindAsync(id);
            db.PhotoViewImage.Remove(photoViewImage);
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
