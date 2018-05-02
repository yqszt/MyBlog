using Microsoft.AspNetCore.Mvc;

namespace My_Blog.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin", Users = "Admin")]
    [Area("Admin")]
    public class ManageHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}