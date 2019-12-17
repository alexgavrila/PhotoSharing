using Photo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace Photo.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult New()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var userProfile = new UserProfile();
            userProfile.BirthDate = currentUser.BirthDate;
            userProfile.FirstName = currentUser.FirstName;
            userProfile.LastName = currentUser.LastName;
            userProfile.TextProfil = currentUser.TextProfil;
            return View(userProfile);
        }



        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult New(UserProfile user)
        {

            return View(user);
        }

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