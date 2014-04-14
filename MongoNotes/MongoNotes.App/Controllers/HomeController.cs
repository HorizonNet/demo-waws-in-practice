using System.Web.Mvc;
using MongoNotes.App.DAL;
using MongoNotes.App.Models;

namespace MongoNotes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly Dal _dal = new Dal();

        public ActionResult Index()
        {
            return View(_dal.GetAllNotes());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Note note)
        {
            try
            {
                _dal.CreateNote(note);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}