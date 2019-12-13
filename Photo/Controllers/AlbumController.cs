using Photo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photo.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Show()
        {
            
            return RedirectToAction("Index", "Image");
        }


        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Album album)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Albums.Add(album);
                    db.SaveChanges();
                    TempData["message"] = "Albumul a fost adaugat!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(album);
                }
            }
            catch (Exception e)
            {
                return View(album);
            }
        }


        public ActionResult Edit(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);

        }

        [HttpPut]
        public ActionResult Edit(int id, Album requestAlbum)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Album album = db.Albums.Find(id);
                    if (TryUpdateModel(album))
                    {
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestAlbum);
                }

            }
            catch (Exception e)
            {
                return View(requestAlbum);
            }
        }




        [HttpDelete]
        public ActionResult Delete (int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            TempData["message"] = "Albumul a fost sters";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}