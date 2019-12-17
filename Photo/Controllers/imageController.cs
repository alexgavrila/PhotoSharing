using Microsoft.AspNet.Identity;
using Photo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photo.Controllers
{

    [Authorize]
    public class ImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private int _perPage = 6;
        // GET: Photo
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var images = db.Images.Include("Category").Include("User").OrderBy(a => a.Date);
            var totalItems = images.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedImages = images.Skip(offset).Take(this._perPage);

          

            ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Images = paginatedImages;




            return View();

        }

        public ActionResult Show(int id)
        {
            Image image = db.Images.Find(id);
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
            ViewBag.comments = db.Comments.Include("User").Where(a => a.PhotoId == id);

            return View(image);
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult New(int id)
        {

            Image image = new Image();

            image.AlbumId = id;
            image.Categories = GetAllCategories();
            image.UserId = User.Identity.GetUserId();

            return View(image);
        }
        [Authorize(Roles = "User,Administrator")]
        [HttpPost]
        public ActionResult New(Image image, HttpPostedFileBase file)
        {
            image.Categories = GetAllCategories();
            try
            {
                if (ModelState.IsValid)    // Protect content from XSS
                {
                    var id = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = String.Concat(id, extension);
                    var physicalPath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(physicalPath))
                        Directory.CreateDirectory(physicalPath);

                    string physicalFullPath = Path.Combine(physicalPath, fileName);
                    file.SaveAs(physicalFullPath);
                    image.filePath = String.Concat("/Uploads/", fileName);
                    db.Images.Add(image);
                    db.SaveChanges();
                    TempData["message"] = "Imaginea a fost adaugata!";
                    return RedirectToAction("Show", "Album", new { id = image.AlbumId });
                }
                else
                {

                    return View(image);
                }
            }
            catch (Exception e)
            {
                return View(image);
            }
        }
        [Authorize(Roles = "User,Administrator")]
        public ActionResult Edit(int id)
        {
            Image image = db.Images.Find(id);
            ViewBag.Image = image;
            image.Categories = GetAllCategories();

            if (image.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View(image);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei poze care nu va apartine!";
                return RedirectToAction("Index");
            }

        }

        [HttpPut]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Image requestImage)
        {
            requestImage.Categories = GetAllCategories();

            try
            {
                if (ModelState.IsValid)
                {
                    Image image = db.Images.Find(id);
                    if (image.UserId == User.Identity.GetUserId() ||
                        User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(image))
                        {
                            db.SaveChanges();
                            TempData["message"] = "Articolul a fost modificat!";
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui articol care nu va apartine!";
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    return View(requestImage);
                }

            }
            catch (Exception e)
            {
                return View(requestImage);
            }
        }



        [Authorize(Roles = "User,Administrator")]

        [HttpDelete]
        //[Authorize(Roles = "")]
        public ActionResult Delete(int id)
        {
            Image image = db.Images.Find(id);
            var albumId = image.AlbumId;
            if (image.UserId == User.Identity.GetUserId() ||
                User.IsInRole("Administrator"))
            {
                db.Images.Remove(image);
                db.SaveChanges();
                TempData["message"] = "Imaginea a fost stearsa!";
                return RedirectToAction("Show", "Album", new { id = albumId});
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti o imaginea care nu va apartine!";
                return RedirectToAction("Show", "Image", new { id = id });
            }

        }






        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();

            // Extragem toate categoriile din baza de date
            var categories = from cat in db.Categories
                             select cat;

            // iteram prin categorii
            foreach (var category in categories)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName.ToString()
                });
            }

            // returnam lista de categorii
            return selectList;
        }


    }
}