using Photo.Models;
using System.Linq;
using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace Photo.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }
            var userCurent = User.Identity.GetUserId();
            var albums = db.Albums.Include("User").OrderBy(e => e.Name).Where(e => e.UserId == userCurent).ToList();
            ViewBag.Albums = albums;
            return View();
        }



        public ActionResult Show(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }


        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Album album)
        {
            var userCurent = User.Identity.GetUserId();
            album.UserId = userCurent;

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
                    Debug.WriteLine(album.Date);

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
        public ActionResult Delete(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            TempData["message"] = "Albumul a fost sters";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}