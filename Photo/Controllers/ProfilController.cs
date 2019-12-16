using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photo.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult New()
        {
            return View();
        }


        /*
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult New(user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(cat);
                    db.SaveChanges();
                    TempData["message"] = "Categoria a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(cat);
                }
            }
            catch (Exception e)
            {
                return View(cat);
            }
        }
        */
        public ActionResult Show(int id)
        {



            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }





    }
}