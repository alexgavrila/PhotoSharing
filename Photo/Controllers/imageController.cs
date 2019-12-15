﻿using Microsoft.AspNet.Identity;
using Photo.Models;
using System;
using System.Collections.Generic;
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

        private int _photoPage = 10;
        // GET: Photo
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Show(int id)
        {
            Image image = db.Images.Find(id);
            ViewBag.utilizatorCurent = User.Identity.GetUserId();


            return View(image);
        }


        public ActionResult New()
        {

            Image image = new Image();
            image.UserId = User.Identity.GetUserId();

            return View(image);
        }

        [HttpPost]
        public ActionResult New(Image image, HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            System.Diagnostics.Debug.WriteLine(fileName);
            System.Diagnostics.Debug.WriteLine(image.Description);
            try
            {
                if (ModelState.IsValid)
                {
                    // Protect content from XSS
                    db.Images.Add(image);   
                    db.SaveChanges();
                    TempData["message"] = "Imaginea a fost adaugata!";
                    return RedirectToAction("Index");
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





        [HttpDelete]
        //[Authorize(Roles = "")]
        public ActionResult Delete(int id)
        {
            Image image = db.Images.Find(id);
            if (image.UserId == User.Identity.GetUserId() ||
                User.IsInRole("Administrator"))
            {
                db.Images.Remove(image);
                db.SaveChanges();
                TempData["message"] = "Imaginea a fost stearsa!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti o imaginea care nu va apartine!";
                return RedirectToAction("Index");
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