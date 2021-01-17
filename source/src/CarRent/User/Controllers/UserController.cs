using Microsoft.AspNetCore.Mvc;

namespace CarRent.User.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
