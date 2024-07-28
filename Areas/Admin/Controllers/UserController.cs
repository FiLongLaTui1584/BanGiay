using System.Linq;
using System.Web.Mvc;
using BanGiay.Context;

namespace BanGiay.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        CNPMEntities5 database = new CNPMEntities5();

        // GET: Admin/User
        public ActionResult Index()
        {
            var users = database.Users.ToList();
            return View(users);
        }

        // GET: Admin/User/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                database.Users.Add(user);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/User/Search
        public PartialViewResult Search(string query)
        {
            var users = database.Users
                .Where(u => u.TenTK.Contains(query) || u.Email.Contains(query))
                .ToList();

            return PartialView("_UserListPartial", users);
        }
    }
}
