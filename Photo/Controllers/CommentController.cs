using Photo.Models;
using System;
using System.Web.Mvc;

namespace Photo.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Comment com)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Comments.Add(com);
                    db.SaveChanges();
                    TempData["message"] = "Comentariu a fost adaugat!";
                    return RedirectToAction("Show", "Image", new { id = com.PhotoId });
                }
                else
                {
                    return RedirectToAction("Show", "Image", new { id = com.PhotoId});
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Show", "Image",new { id = com.PhotoId });
            }
        }



        public ActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }

        [HttpPut]

        public ActionResult Edit(int id, Comment requestComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Comment comment = db.Comments.Find(id);
                    if (TryUpdateModel(comment))
                    {

                        TempData["message"] = "Comentariu a fost modificat!";
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestComment);
                }
            }
            catch (Exception e)
            {
                return View(requestComment);
            }
        }






        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            var photoId = comment.PhotoId;
            db.Comments.Remove(comment);
            TempData["message"] = "Comentariu a fost stears!";
            db.SaveChanges();
            return RedirectToAction("Show", "Image", new { id = photoId });
        }




    }
}