﻿using Photo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace Photo.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        // GET: Profil
        private ApplicationDbContext db = new ApplicationDbContext();

        


        public ActionResult New()
        {
            
            UserProfile user = new UserProfile();

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            user.FirstName = currentUser.FirstName;
            user.LastName = currentUser.LastName;
            user.BirthDate = currentUser.BirthDate;
            user.TextProfil = currentUser.TextProfil;

            return View(user);
        }



        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult New(UserProfile user)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            try
            {
                if (ModelState.IsValid)
                {
                    currentUser.FirstName = user.FirstName;
                    currentUser.LastName = user.LastName;
                    currentUser.BirthDate = user.BirthDate;
                    currentUser.TextProfil = user.TextProfil;
                    
                    db.SaveChanges();
                    TempData["message"] = "Profil creat!";
                    return RedirectToAction("Index","Manage");
                }
                else
                {
                    return View(user);
                }
            }
            catch (Exception e)
            {
                return View(user);
            }
        }
    }
}