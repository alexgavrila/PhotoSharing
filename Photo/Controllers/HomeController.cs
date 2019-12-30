using Photo.Models;
using System.Linq;
using System.Web.Mvc;

namespace Photo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.images = db.Images.Include("Category").Include("User").Include("Album").OrderBy(a => a.Date).Take(10);
            
            return View();




        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string key)
        {
            ViewBag.images = db.Images.Include("Category").Include("User").Include("Album").Where(a => a.Description.Contains(key)).OrderBy(a => a.Date).Take(10);

            return View();
        }
    }
}